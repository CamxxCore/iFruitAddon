using System.Collections.Generic;
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

            if (Function.Call<int>(Hash._GET_NUMBER_OF_INSTANCES_OF_STREAMED_SCRIPT, _mScriptHash) > 0)
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
                }
            }

            _shouldDraw = false;
        }

        internal void DisplayCallUI(int handle, string contactName, string picName = "CELL_300")
        {
            Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION, handle, "SET_DATA_SLOT");
            Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_INT, 4);
            Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_INT, 0);
            Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_INT, 3);

            Function.Call(Hash._BEGIN_TEXT_COMPONENT, "STRING");
            Function.Call(Hash._0x761B77454205A61D, contactName, -1);
            Function.Call(Hash._END_TEXT_COMPONENT);

            Function.Call(Hash._BEGIN_TEXT_COMPONENT, "CELL_2000");
            Function.Call(Hash._ADD_TEXT_COMPONENT_STRING, picName);
            Function.Call(Hash._END_TEXT_COMPONENT);

            Function.Call(Hash._BEGIN_TEXT_COMPONENT, "STRING");
            Function.Call(Hash._0x761B77454205A61D, "DIALING...", -1);
            Function.Call(Hash._END_TEXT_COMPONENT);

            Function.Call(Hash._POP_SCALEFORM_MOVIE_FUNCTION_VOID);

            Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION, handle, "DISPLAY_VIEW");
            Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_INT, 4);
            Function.Call(Hash._POP_SCALEFORM_MOVIE_FUNCTION_VOID);
        }

        internal int GetSelectedIndex(int handle)
        {
            Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION, handle, "GET_CURRENT_SELECTION");
            int num = Function.Call<int>(Hash._POP_SCALEFORM_MOVIE_FUNCTION);
            while (!Function.Call<bool>(Hash._0x768FF8961BA904D6, num))
                Script.Wait(0);
            int data = Function.Call<int>(Hash._0x2DE7EFA66B906036, num);
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
    }
}