using GTA.Native;
using System;
using GTA;

namespace iFruitAddon
{
    public class iFruitContact
    {
        /// <summary>
        /// Fired when the contact is selected in the contacts app.
        /// </summary>
        /// 
        [Obsolete("This method will be removed. Use the Answered event instead.")]
        public event ContactSelectedEvent Selected;

        /// <summary>
        /// Fired when the contact picks up the phone.
        /// </summary>
        public event ContactAnsweredEvent Answered;

        /// <summary>
        /// The name of the contact.
        /// </summary>
        public string Name { get; set; } = "";

        /// <summary>
        /// The index where we should draw the item.
        /// </summary>
        public int Index { get; private set; } = 0;

        /// <summary>
        /// Status representing the outcome when the contact is called. 
        /// Contact will answer when true.
        /// </summary>
        public bool Active { get; set; } = true;

        /// <summary>
        /// Milliseconds timeout before the contact picks up. 
        /// Set this to 0 if you want the contact to answer instantly.
        /// </summary>
        public int DialTimeout { get; set; } = 0;

        /// <summary>
        /// The icon to associate with this contact.
        /// </summary>
        public ContactIcon Icon { get; set; } = ContactIcon.Generic;

        public iFruitContact(string name, int index)
        {
            Name = name;
            Index = index;
        }

        internal virtual void OnSelected(iFruitContactCollection sender)
        {
            if (Selected != null)
                Selected(sender, this);
        }

        protected virtual void OnAnswered(iFruitContact sender)
        {
            if (Answered != null)
                Answered(this);
        }

        internal void Draw(int handle)
        {
            Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION, handle, "SET_DATA_SLOT");
            Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_INT, 2);
            Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_INT, Index);
            Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_INT, 0);
            Function.Call(Hash._BEGIN_TEXT_COMPONENT, "STRING");
            Function.Call(Hash._ADD_TEXT_COMPONENT_STRING, Name);
            Function.Call(Hash._END_TEXT_COMPONENT);
            Function.Call(Hash._BEGIN_TEXT_COMPONENT, "CELL_999");
            Function.Call(Hash._END_TEXT_COMPONENT);
            Function.Call(Hash._BEGIN_TEXT_COMPONENT, "CELL_2000");
            Function.Call(Hash._ADD_TEXT_COMPONENT_STRING, Icon.Name);
            Function.Call(Hash._END_TEXT_COMPONENT);
            Function.Call(Hash._POP_SCALEFORM_MOVIE_FUNCTION_VOID);
        }

        private bool _dialActive, _busyActive;
        private int _dialSoundID = -1;
        private int _busySoundID = -1;
        private int _callTimer, _busyTimer;

        internal void Update()
        {
            if (_busyActive && Game.GameTime > _busyTimer)
            {
                Game.Player.Character.Task.PutAwayMobilePhone();
                Function.Call(Hash.STOP_SOUND, _busySoundID);
                Function.Call(Hash.RELEASE_SOUND_ID, _busySoundID);
                _busySoundID = -1;
                _busyActive = false;
            }

            if (_dialActive && Game.GameTime > _callTimer)
            {
                Function.Call(Hash.STOP_SOUND, _dialSoundID);
                Function.Call(Hash.RELEASE_SOUND_ID, _dialSoundID);
                _dialSoundID = -1;

                if (!Active)
                {
                    _busySoundID = Function.Call<int>(Hash.GET_SOUND_ID);
                    Function.Call(Hash.PLAY_SOUND_FRONTEND, _busySoundID, "Remote_Engaged", "Phone_SoundSet_Default", 1);
                    _busyTimer = Game.GameTime + 5000;
                    _busyActive = true;
                }

                else
                    OnAnswered(this);

                _dialActive = false;
            }
        }

        public void Call()
        {
            if (_dialActive || _busyActive)
                return;

            Game.Player.Character.Task.UseMobilePhone();

            if (DialTimeout > 0)
            {
                _dialSoundID = Function.Call<int>(Hash.GET_SOUND_ID);
                Function.Call(Hash.PLAY_SOUND_FRONTEND, _dialSoundID, "Dial_and_Remote_Ring", "Phone_SoundSet_Default", 1);
                _callTimer = Game.GameTime + DialTimeout;
                _dialActive = true;
            }

            else
                OnAnswered(this);
        }

        public void EndCall()
        {
            if (_dialActive)
            {
                Function.Call(Hash.STOP_SOUND, _dialSoundID);
                Function.Call(Hash.RELEASE_SOUND_ID, _dialSoundID);
                _dialSoundID = -1;
                _dialActive = false;
            }

            if (_busyActive)
            {
                Function.Call(Hash.STOP_SOUND, _busySoundID);
                Function.Call(Hash.RELEASE_SOUND_ID, _busySoundID);
                _busySoundID = -1;
                _busyActive = false;
            }
        }
    }
}