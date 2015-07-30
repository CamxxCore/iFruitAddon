using GTA.Native;
using GTA;

namespace iFruitContacts
{
    public class iFruitContact
    {
        public event ContactSelectedEvent Selected;

        /// <summary>
        /// The name of the contact.
        /// </summary>
        private string name;
        public string Name
        {
            get { return name; }
        }

        /// <summary>
        /// The index where we should draw the item.
        /// </summary>
        private int index;
        public int Index
        {
            get { return index; }
        }

        private int _mHandle;

        public iFruitContact(string name, int index)
        {
            this.name = name;
            this.index = index;
            this._mHandle = 0;
        }

        internal virtual void OnSelected(iFruitContactCollection sender)
        {
            if (Selected != null)
                Selected(sender, this);
        }

        public void Draw()
        {
            _mHandle = RefreshActiveMobile();

            Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION, _mHandle, "SET_DATA_SLOT");
            Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_FLOAT, Function.Call<float>(Hash.TO_FLOAT, 2));
            Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_FLOAT, Function.Call<float>(Hash.TO_FLOAT, index));
            Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_FLOAT, 0.0f);
            Function.Call(Hash._BEGIN_TEXT_COMPONENT, "STRING");
            Function.Call(Hash._ADD_TEXT_COMPONENT_STRING, name);
            Function.Call(Hash._END_TEXT_COMPONENT);
            Function.Call(Hash._BEGIN_TEXT_COMPONENT, "CELL_MP_999");
            Function.Call(Hash._END_TEXT_COMPONENT);
            Function.Call(Hash._BEGIN_TEXT_COMPONENT, "CELL_MP_999");
            Function.Call(Hash._END_TEXT_COMPONENT);
            Function.Call(Hash._POP_SCALEFORM_MOVIE_FUNCTION_VOID);
        }

        public int RefreshActiveMobile()
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
}