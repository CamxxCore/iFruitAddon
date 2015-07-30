using System.Collections.Generic;
using GTA.Native;
using GTA;

namespace iFruitContacts
{
    public delegate void ContactSelectedEvent(iFruitContactCollection sender, iFruitContact selectedItem);

    public class iFruitContactCollection : List<iFruitContact>
    {
        private int _mHash, _mHandle;

        public iFruitContactCollection()
        {
            this._mHash = Function.Call<int>(Hash.GET_HASH_KEY, "appcontacts");
            this._mHandle = RefreshActiveMobile();
        }

        private bool _shouldDraw = true;

        public void Update()
        {
            if (Function.Call<int>(Hash._GET_NUMBER_OF_INSTANCES_OF_STREAMED_SCRIPT, _mHash) > 0)
            {
                int index = GetSelectedIndex();

                if (_shouldDraw)
                {
                    Script.Wait(10);
                    base.ForEach(x => x.Draw());
                    _shouldDraw = !_shouldDraw;
                }

                base.ForEach(x =>
                {
                    if (index == x.Index)
                    {
                        DisableControl(176);
                        if (GetControl(176))
                            x.OnSelected(this);
                    }
                });

            }
            else
            {
                _shouldDraw = true;
            }

            if (_mHandle != RefreshActiveMobile())
                _mHandle = RefreshActiveMobile();
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

        public int GetSelectedIndex()
        {
            Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION, _mHandle, "GET_CURRENT_SELECTION");
            int num = Function.Call<int>(Hash._POP_SCALEFORM_MOVIE_FUNCTION);
            while (!Function.Call<bool>(Hash._0x768FF8961BA904D6, num))
                Script.Wait(0);
            int data = Function.Call<int>(Hash._0x2DE7EFA66B906036, num);
            return data;
        }

        private bool GetControl(int control)
        {
            return Function.Call<bool>(Hash.IS_DISABLED_CONTROL_PRESSED, 0, control);
        }

        private void DisableControl(int control)
        {
            Function.Call(Hash.DISABLE_CONTROL_ACTION, 0, control, false);
        }
    }
}