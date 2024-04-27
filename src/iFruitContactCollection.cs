using GTA.Native;
using GTA;

namespace iFruitAddon
{
    public class iFruitContactCollection : List<iFruitContact>
    {
        public iFruitContactCollection()
        {
            _mScriptHash = Function.Call<int>(Hash.GET_HASH_KEY, "appcontacts");
        }

        private bool _shouldDraw = true;
        private int _mScriptHash;

        internal void Update(int handle)
        {
            int index = -1;

            if (Function.Call<int>(Hash.GET_NUMBER_OF_THREADS_RUNNING_THE_SCRIPT_WITH_THIS_HASH, _mScriptHash) > 0)
            {
                _shouldDraw = true;

                if (Function.Call<bool>(Hash.IS_CONTROL_PRESSED, 2, 176))
                {
                    index = GetSelectedIndex(handle);
                }
            }

            foreach (var contact in this)
            {
                contact.Update();

                if (_shouldDraw)
                {
                    contact.Draw(handle);
                }

                if (index != -1 && index == contact.Index)
                {
                    contact.Call();
                    contact.OnSelected(this);
                    DisplayCallUI(handle, contact.Name, contact.Icon.Name);
                    Script.Wait(5);            
                    RemoveActiveNotification();
                }
            }

            _shouldDraw = false;
        }

        internal void DisplayCallUI(int handle, string contactName, string picName = "CELL_300")
        {
            Function.Call(Hash.BEGIN_SCALEFORM_MOVIE_METHOD, handle, "SET_DATA_SLOT");
            Function.Call(Hash.SCALEFORM_MOVIE_METHOD_ADD_PARAM_INT, 4);
            Function.Call(Hash.SCALEFORM_MOVIE_METHOD_ADD_PARAM_INT, 0);
            Function.Call(Hash.SCALEFORM_MOVIE_METHOD_ADD_PARAM_INT, 3);

            Function.Call(Hash.BEGIN_TEXT_COMMAND_SCALEFORM_STRING, "STRING");
            Function.Call(Hash.END_TEXT_COMMAND_SCALEFORM_STRING, contactName, -1);
            Function.Call(Hash.END_TEXT_COMMAND_SCALEFORM_STRING);

            Function.Call(Hash.BEGIN_TEXT_COMMAND_SCALEFORM_STRING, "CELL_2000");
            Function.Call(Hash.ADD_TEXT_COMPONENT_SUBSTRING_TEXT_LABEL, picName);
            Function.Call(Hash.END_TEXT_COMMAND_SCALEFORM_STRING);

            Function.Call(Hash.BEGIN_TEXT_COMMAND_SCALEFORM_STRING, "STRING");
            Function.Call(Hash.END_TEXT_COMMAND_SCALEFORM_STRING, "DIALING...", -1);
            Function.Call(Hash.END_TEXT_COMMAND_SCALEFORM_STRING);

            Function.Call(Hash.END_SCALEFORM_MOVIE_METHOD);

            Function.Call(Hash.BEGIN_SCALEFORM_MOVIE_METHOD, handle, "DISPLAY_VIEW");
            Function.Call(Hash.SCALEFORM_MOVIE_METHOD_ADD_PARAM_INT, 4);
            Function.Call(Hash.END_SCALEFORM_MOVIE_METHOD);
        }

        internal int GetSelectedIndex(int handle)
        {
            Function.Call(Hash.BEGIN_SCALEFORM_MOVIE_METHOD, handle, "GET_CURRENT_SELECTION");
            int num = Function.Call<int>(Hash.END_SCALEFORM_MOVIE_METHOD_RETURN_VALUE);
            while (!Function.Call<bool>(Hash.IS_SCALEFORM_MOVIE_METHOD_RETURN_VALUE_READY, num))
                Script.Wait(0);
            int data = Function.Call<int>(Hash.GET_SCALEFORM_MOVIE_METHOD_RETURN_VALUE_INT, num);
            return data;
        }

        internal bool GetControl(int control)
        {
            return Function.Call<bool>(Hash.IS_DISABLED_CONTROL_PRESSED, 2, control);
        }

        internal void DisableControl(int control)
        {
            Function.Call(Hash.DISABLE_CONTROL_ACTION, 2, control, false);
        }

        internal void RemoveActiveNotification()
        {
            Function.Call(Hash.BEGIN_TEXT_COMMAND_THEFEED_POST, "STRING");
            Function.Call(Hash.ADD_TEXT_COMPONENT_SUBSTRING_TEXT_LABEL, "temp");
            int temp = Function.Call<int>(Hash.END_TEXT_COMMAND_THEFEED_POST_TICKER, false, 1);
            Function.Call(Hash.THEFEED_REMOVE_ITEM, temp);
            Function.Call(Hash.THEFEED_REMOVE_ITEM, temp - 1);
        }
    }
}