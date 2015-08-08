using GTA.Native;
using GTA;

namespace iFruitAddon
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

        public iFruitContact(string name, int index)
        {
            this.name = name;
            this.index = index;
        }

        internal virtual void OnSelected(iFruitContactCollection sender)
        {
            if (Selected != null)
                Selected(sender, this);
        }

        public void Draw(int handle)
        {
            Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION, handle, "SET_DATA_SLOT");
            Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_FLOAT, 2.0f);
            Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_FLOAT, (float)index);
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
    }
}