using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using DirectShowLib;
using WPFMediaKit.DirectShow.MediaPlayers;
using FilterCategory = WPFMediaKit.DirectShow.MediaPlayers.FilterCategory;

namespace WPFMediaKit.MediaFoundation
{
    public class VideoCaptureUtils:MediaPlayerBase
    {

        public static List<AMMediaType> GetVideoCaptureCap(string devicePath)
        {
            List<AMMediaType> types = new List<AMMediaType>();
            try
            {
                if (string.IsNullOrEmpty(devicePath))
                    throw new ArgumentNullException("devicePath");

                var m_graph = (IGraphBuilder) new FilterGraphNoThread();
                var graphBuilder = (ICaptureGraphBuilder2) new CaptureGraphBuilder2();

                int hr = graphBuilder.SetFiltergraph(m_graph);
                DsError.ThrowExceptionForHR(hr);

                var m_captureDevice = AddFilterByDevicePath(m_graph, FilterCategory.VideoInputDevice, devicePath);

                if (m_captureDevice == null)
                    throw new Exception(string.Format("Capture device {0} not found or could not be created", devicePath));

                /* The stream config interface */
                object streamConfig;

                /* Get the stream's configuration interface */
                hr = graphBuilder.FindInterface(PinCategory.Capture,
                    MediaType.Video,
                    m_captureDevice,
                    typeof (IAMStreamConfig).GUID,
                    out streamConfig);

                DsError.ThrowExceptionForHR(hr);

                var videoStreamConfig = streamConfig as IAMStreamConfig;

                /* If QueryInterface fails... */
                if (videoStreamConfig == null)
                {
                    throw new Exception("Failed to get IAMStreamConfig");
                }

                int capNum, capSize;
                videoStreamConfig.GetNumberOfCapabilities(out capNum, out capSize);
                IntPtr TaskMemPointer = Marshal.AllocCoTaskMem(capSize);
                for (int i = 0; i < capNum; i++)
                {
                    AMMediaType type;
                    videoStreamConfig.GetStreamCaps(i, out type, TaskMemPointer);
                    if (type != null)
                        types.Add(type);
                }

                Marshal.ReleaseComObject(graphBuilder);
                Marshal.ReleaseComObject(m_graph);
                Marshal.ReleaseComObject(m_captureDevice);
            }
            catch (Exception)
            {

            }
            return types;
        }  
    }
}
