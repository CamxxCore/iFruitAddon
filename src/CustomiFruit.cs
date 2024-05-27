using System.Drawing;
using GTA;
using GTA.Native;

namespace iFruitAddon
{
    public delegate void ContactSelectedEvent(iFruitContactCollection sender, iFruitContact selectedItem);

    public delegate void ContactAnsweredEvent(iFruitContact contact);

    public class CustomiFruit
    {
        /// <summary>
        /// Left Button Color
        /// </summary>
        public Color LeftButtonColor { get; set; } = Color.Empty;

        /// <summary>
        /// Center Button Color
        /// </summary>
        public Color CenterButtonColor { get; set; } = Color.Empty;

        /// <summary>
        /// Right Button Color
        /// </summary>
        public Color RightButtonColor { get; set; } = Color.Empty;

        /// <summary>
        /// Left Button Icon
        /// </summary>
        public SoftKeyIcon LeftButtonIcon { get; set; } = SoftKeyIcon.Blank;

        /// <summary>
        /// Center Button Icon
        /// </summary>
        public SoftKeyIcon CenterButtonIcon { get; set; } = SoftKeyIcon.Blank;

        /// <summary>
        /// Right Button Icon
        /// </summary>
        public SoftKeyIcon RightButtonIcon { get; set; } = SoftKeyIcon.Blank;

        /// <summary>
        /// List of custom contacts in the phone
        /// </summary>
        public iFruitContactCollection Contacts
        {
            get { return _contacts; }
            set { _contacts = value; }
        }

        public CustomiFruit() : this(new iFruitContactCollection())
        { }

        /// <summary>
        /// Initialize the class.
        /// </summary>
        /// <param name="contacts"></param>
        public CustomiFruit(iFruitContactCollection contacts)
        {
            _contacts = contacts;
            _mScriptHash = Function.Call<int>(Hash.GET_HASH_KEY, "cellphone_flashhand");
        }

        /// <summary>
        /// Handle of the current scaleform.
        /// </summary>
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

        /// <summary>
        /// Set text displayed at the top of the phone interface. Must be called every update!
        /// </summary>
        /// <param name="text"></param>
        public void SetTextHeader(string text)
        {
            Function.Call(Hash.BEGIN_SCALEFORM_MOVIE_METHOD, Handle, "SET_HEADER");
            Function.Call(Hash.BEGIN_TEXT_COMMAND_SCALEFORM_STRING, "STRING");
            Function.Call(Hash.END_TEXT_COMMAND_SCALEFORM_STRING, text, -1);
            Function.Call(Hash.END_TEXT_COMMAND_SCALEFORM_STRING);
            Function.Call(Hash.END_SCALEFORM_MOVIE_METHOD);
        }

        /// <summary>
        /// Set icon of the soft key buttons directly.
        /// </summary>
        /// <param name="buttonID">The button index</param>
        /// <param name="icon">Supplied icon</param>
        public void SetSoftKeyIcon(int buttonID, SoftKeyIcon icon)
        {
            Function.Call(Hash.BEGIN_SCALEFORM_MOVIE_METHOD, Handle, "SET_SOFT_KEYS");
            Function.Call(Hash.SCALEFORM_MOVIE_METHOD_ADD_PARAM_INT, buttonID);
            Function.Call(Hash.SCALEFORM_MOVIE_METHOD_ADD_PARAM_BOOL, true);
            Function.Call(Hash.SCALEFORM_MOVIE_METHOD_ADD_PARAM_INT, (int)icon);
            Function.Call(Hash.END_SCALEFORM_MOVIE_METHOD);
        }

        /// <summary>
        /// Set the color of the soft key buttons directly.
        /// </summary>
        /// <param name="buttonID">The button index</param>
        /// <param name="color">Supplied color</param>
        public void SetSoftKeyColor(int buttonID, Color color)
        {
            Function.Call(Hash.BEGIN_SCALEFORM_MOVIE_METHOD, Handle, "SET_SOFT_KEYS_COLOUR");
            Function.Call(Hash.SCALEFORM_MOVIE_METHOD_ADD_PARAM_INT, buttonID);
            Function.Call(Hash.SCALEFORM_MOVIE_METHOD_ADD_PARAM_INT, color.R);
            Function.Call(Hash.SCALEFORM_MOVIE_METHOD_ADD_PARAM_INT, color.G);
            Function.Call(Hash.SCALEFORM_MOVIE_METHOD_ADD_PARAM_INT, color.B);
            Function.Call(Hash.END_SCALEFORM_MOVIE_METHOD);
        }

        internal void SetWallpaperTXD(string textureDict)
        {
            Function.Call(Hash.BEGIN_SCALEFORM_MOVIE_METHOD, Handle, "SET_BACKGROUND_CREW_IMAGE");
            Function.Call(Hash.BEGIN_TEXT_COMMAND_SCALEFORM_STRING, "CELL_2000");
            Function.Call(Hash.ADD_TEXT_COMPONENT_SUBSTRING_TEXT_LABEL, textureDict);
            Function.Call(Hash.END_TEXT_COMMAND_SCALEFORM_STRING);
            Function.Call(Hash.END_SCALEFORM_MOVIE_METHOD);
        }

        public void SetWallpaper(Wallpaper wallpaper)
        {
            _wallpaper = wallpaper;
        }

        public void SetWallpaper(ContactIcon icon)
        {
            _wallpaper = icon;
        }

        public void SetWallpaper(string textureDict)
        {
            _wallpaper = new Wallpaper(textureDict);
        }

        private bool _shouldDraw = true;    
        private PhoneImage _wallpaper;
        private iFruitContactCollection _contacts;
        private int _mScriptHash;

        public void Update()
        {
            if (Function.Call<int>(Hash.GET_NUMBER_OF_THREADS_RUNNING_THE_SCRIPT_WITH_THIS_HASH, _mScriptHash) > 0)
            {
                if (_shouldDraw)
                {
                    Script.Wait(0);

                    if (LeftButtonColor != Color.Empty)
                        SetSoftKeyColor(1, LeftButtonColor);
                    if (CenterButtonColor != Color.Empty)
                        SetSoftKeyColor(2, CenterButtonColor);
                    if (RightButtonColor != Color.Empty)
                        SetSoftKeyColor(3, RightButtonColor);

                    Script.Wait(0);

                    if (LeftButtonIcon != SoftKeyIcon.Blank)
                        SetSoftKeyIcon(1, LeftButtonIcon);
                    if (CenterButtonIcon != SoftKeyIcon.Blank)
                        SetSoftKeyIcon(2, CenterButtonIcon);
                    if (RightButtonIcon != SoftKeyIcon.Blank)
                        SetSoftKeyIcon(3, RightButtonIcon);

                    if (_wallpaper != null)
                        SetWallpaperTXD(_wallpaper.Name);

                    _shouldDraw = !_shouldDraw;
                }  
            }

            else
            {
                _shouldDraw = true;
            }

            _contacts.Update(Handle);
        }
    }

    public enum SoftKeyIcon
    {
        Blank = 1,
        Select = 2,
        Pages = 3,
        Back = 4,
        Call = 5,
        Hangup = 6,
        HangupHuman = 7,
        Week = 8,
        Keypad = 9,
        Open = 10,
        Reply = 11,
        Delete = 12,
        Yes = 13,
        No = 14,
        Sort = 15,
        Website = 16,
        Police = 17,
        Ambulance = 18,
        Fire = 19,
        Pages2 = 20
    }
}
