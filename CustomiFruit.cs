using System.Drawing;
using GTA;
using GTA.Native;

namespace iFruitAddon
{
    public class CustomiFruit
    {
        private int _mHash;

        /// <summary>
        /// List of custom contacts in the phone
        /// </summary>
        private iFruitContactCollection contacts;
        public iFruitContactCollection Contacts
        {
            get { return contacts; }
            set { this.contacts = value; }
        }

        /// <summary>
        /// Keypad Button Color
        /// </summary>
        ///
        private Color keypadButtonColor = Color.Empty;
        public Color KeypadButtonColor
        {
            get { return keypadButtonColor; }
            set { this.keypadButtonColor = value; }
        }

        /// <summary>
        /// Select Button Color
        /// </summary>
        ///
        private Color selectButtonColor = Color.Empty;
        public Color SelectButtonColor
        {
            get { return selectButtonColor; }
            set{ this.selectButtonColor = value; }
        }

        /// <summary>
        /// Return Button Color
        /// </summary>
        ///
        private Color returnButtonColor = Color.Empty;
        public Color ReturnButtonColor
        {
            get { return returnButtonColor; }
            set { this.returnButtonColor = value; }
        }

        public int Handle
        {
            get
            {
                var model = (uint)Game.Player.Character.Model.Hash;
                switch (model)
                {
                    case (uint)PedHash.Michael:
                        return Function.Call<int>(Hash.REQUEST_SCALEFORM_MOVIE, "cellphone_ifruit");
                    case (uint)PedHash.Franklin:
                        return Function.Call<int>(Hash.REQUEST_SCALEFORM_MOVIE, "cellphone_badger");
                    case (uint)PedHash.Trevor:
                        return Function.Call<int>(Hash.REQUEST_SCALEFORM_MOVIE, "cellphone_facade");
                    default: return Function.Call<int>(Hash.REQUEST_SCALEFORM_MOVIE, "cellphone_ifruit");
                }
            }
        }

        public CustomiFruit()
        {
            this.contacts = new iFruitContactCollection();
            this._mHash = Function.Call<int>(Hash.GET_HASH_KEY, "cellphone_flashhand");
        }

        public CustomiFruit(iFruitContactCollection contacts)
        {
            this.contacts = contacts;
            this._mHash = Function.Call<int>(Hash.GET_HASH_KEY, "cellphone_flashhand");
        }

        private void SetSoftKeyColor(int key, Color color)
        {
            Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION, Handle, "SET_SOFT_KEYS_COLOUR");
            Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_INT, key);
            Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_INT, color.R);
            Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_INT, color.G);
            Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_INT, color.B);
            Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_INT, Function.Call<int>(Hash.ROUND, 0xbf800000));
            Function.Call(Hash._POP_SCALEFORM_MOVIE_FUNCTION_VOID);
        }

        private bool _shouldDraw = true;

        public void Update()
        {
            if (Function.Call<int>(Hash._GET_NUMBER_OF_INSTANCES_OF_STREAMED_SCRIPT, _mHash) > 0)
            {
                if (_shouldDraw)
                {
                    Script.Wait(10);
                    if (keypadButtonColor != Color.Empty)
                        SetSoftKeyColor(1, keypadButtonColor);
                    if (selectButtonColor != Color.Empty)
                        SetSoftKeyColor(2, selectButtonColor);
                    if (returnButtonColor != Color.Empty)
                        SetSoftKeyColor(3, returnButtonColor);

                    _shouldDraw = !_shouldDraw;
                }

                contacts.Update(Handle);
            }

            else
            {
                _shouldDraw = true;
            }
        }
    }
}
