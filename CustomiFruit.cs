using System.Drawing;
using GTA;
using GTA.Native;

namespace iFruitAddon
{
    public class CustomiFruit
    {
        /// <summary>
        /// Keypad Button Color
        /// </summary>
        public Color KeypadButtonColor { get; set; } = Color.Empty;

        /// <summary>
        /// Select Button Color
        /// </summary>
        public Color SelectButtonColor { get; set; } = Color.Empty;

        /// <summary>
        /// Return Button Color
        /// </summary>
        public Color ReturnButtonColor { get; set; } = Color.Empty;

        /// <summary>
        /// Header text shown in phone UI
        /// </summary>
        public string HeaderText { get; set; } = string.Empty;

        /// <summary>
        /// List of custom contacts in the phone
        /// </summary>
        public iFruitContactCollection Contacts
        {
            get { return _contacts; }
            set { _contacts = value; }
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

        public CustomiFruit() : this(new iFruitContactCollection())
        { }

        public CustomiFruit(iFruitContactCollection contacts)
        {
            _contacts = contacts;
            _mHash = Function.Call<int>(Hash.GET_HASH_KEY, "cellphone_flashhand");
        }

        private void SetSoftKeyColor(int key, Color color)
        {
            Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION, Handle, "SET_SOFT_KEYS_COLOUR");
            Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_INT, key);
            Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_INT, color.R);
            Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_INT, color.G);
            Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_INT, color.B);
            Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_INT, -1);
            Function.Call(Hash._POP_SCALEFORM_MOVIE_FUNCTION_VOID);
        }

        private void SetHeaderString(string text)
        {
            Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION, Handle, "SET_HEADER");
            Function.Call(Hash._BEGIN_TEXT_COMPONENT, "STRING");
            Function.Call(Hash._0x761B77454205A61D, text, -1);
            Function.Call(Hash._END_TEXT_COMPONENT);
            Function.Call(Hash._POP_SCALEFORM_MOVIE_FUNCTION_VOID);
        }

        private bool _shouldDraw = true;
        private iFruitContactCollection _contacts;
        private int _mHash;

        public void Update()
        {
            if (Function.Call<int>(Hash._GET_NUMBER_OF_INSTANCES_OF_STREAMED_SCRIPT, _mHash) > 0)
            {
                if (_shouldDraw)
                {
                    Script.Wait(10);
                    if (KeypadButtonColor != Color.Empty)
                        SetSoftKeyColor(1, KeypadButtonColor);
                    if (SelectButtonColor != Color.Empty)
                        SetSoftKeyColor(2, SelectButtonColor);
                    if (ReturnButtonColor != Color.Empty)
                        SetSoftKeyColor(3, ReturnButtonColor);

                    _shouldDraw = !_shouldDraw;
                }

                if (HeaderText != string.Empty)
                    SetHeaderString(HeaderText);
            }

            else
            {
                _shouldDraw = true;
            }

            _contacts.Update(Handle);
        }
    }
}
