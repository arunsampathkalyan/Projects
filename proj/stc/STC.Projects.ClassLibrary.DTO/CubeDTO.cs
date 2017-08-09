using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;


namespace STC.Projects.ClassLibrary.DTO
{
    [DataContract]
    public class CubeDTO
    {
        [DataMember]
        public string LegendName { get; set; }
        [DataMember]
        public ObservableCollection<CubeDetailsDTO> Details { get; set; }
        [DataMember]
        public ObservableCollection<CubeDetailsDTO> LineDetails { get; set; }
        [DataMember]
        public Brush LegendColor { get; set; }
        [DataMember]
        public List<CubeFacts> StatisticalAnalysis { get; set; }
    }
    [DataContract]
    public class CubeDetailsDTO
    {
        [DataMember]
        public string Key { get; set; }
        [DataMember]
        public double Value { get; set; }
        [DataMember]
        public double TargetValue { get; set; }
        [DataMember]
        public string Percentage { get; set; }
        [DataMember]
        public string Orientation { get; set; }
    }

    public class CubeFacts
    {
        [DataMember]
        public string FactKey { get; set; }
        [DataMember]
        public string FactValue { get; set; }
        [DataMember]
        public string FactColor { get; set; }
    }
}
