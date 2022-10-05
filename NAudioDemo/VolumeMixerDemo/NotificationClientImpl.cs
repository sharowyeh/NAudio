using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using NAudio.CoreAudioApi;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudioDemo.VolumeMixerDemo
{
    public delegate void DefaultDeviceChangedDelegate(DataFlow flow, Role role, string defaultDeviceId);
    public delegate void DeviceAddedDelegate(string deviceId);
    public delegate void DeviceRemovedDelegate(string deviceId);
    public delegate void DeviceStateChangedDelegate(string deviceId, DeviceState newState);
    public delegate void PropertyValueChangedDelegate(string deviceId, PropertyKey key);

    internal class NotificationClientImpl : IMMNotificationClient, IDisposable
    {

        public NotificationClientImpl()
        {

        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public event DefaultDeviceChangedDelegate DefaultDeviceChanged;
        public event DeviceAddedDelegate DeviceAdded;
        public event DeviceRemovedDelegate DeviceRemoved;
        public event DeviceStateChangedDelegate DeviceStateChanged;
        public event PropertyValueChangedDelegate PropertyValueChanged;

        public void OnDefaultDeviceChanged(DataFlow flow, Role role, [MarshalAs(UnmanagedType.LPWStr)] string defaultDeviceId)
        {
            DefaultDeviceChanged?.Invoke(flow, role, defaultDeviceId);
            //System.Diagnostics.Debug.WriteLine($"On default changed {defaultDeviceId}");
        }

        public void OnDeviceAdded([MarshalAs(UnmanagedType.LPWStr)] string pwstrDeviceId)
        {
            DeviceAdded?.Invoke(pwstrDeviceId);
            //System.Diagnostics.Debug.WriteLine($"On device added {pwstrDeviceId}");
        }

        public void OnDeviceRemoved([MarshalAs(UnmanagedType.LPWStr)] string deviceId)
        {
            DeviceRemoved?.Invoke(deviceId);
            //System.Diagnostics.Debug.WriteLine($"On device removed {deviceId}");
        }

        public void OnDeviceStateChanged([MarshalAs(UnmanagedType.LPWStr)] string deviceId, [MarshalAs(UnmanagedType.I4)] DeviceState newState)
        {
            DeviceStateChanged?.Invoke(deviceId, newState);
            //System.Diagnostics.Debug.WriteLine($"On device state changed {deviceId} {newState}");
        }

        public void OnPropertyValueChanged([MarshalAs(UnmanagedType.LPWStr)] string pwstrDeviceId, PropertyKey key)
        {
            PropertyValueChanged?.Invoke(pwstrDeviceId, key);
            //System.Diagnostics.Debug.WriteLine($"On prop value changed {pwstrDeviceId} {key.propertyId}");
        }
    }
}
