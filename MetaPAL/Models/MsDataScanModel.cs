﻿using MassSpectrometry;
using MathNet.Numerics.RootFinding;
using MetaPAL.ControlledVocabulary;
using Microsoft.CodeAnalysis;
using MzLibUtil.NoiseEstimation;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
using ThermoFisher.CommonCore.Data.Business;
using static MetaPAL.ControlledVocabulary.PsiMsTypes;

namespace MetaPAL.Models
{
    [Table("MsDataScans")]
    public class MsDataScanModel
    {
        [Key]
        public int Id { get; set; } //autogenerated unique id

        [ForeignKey("DataFile")]
        public int DataFileId { get; set; }

        #region HupoPsiProperties 
        // The properties in this region all correspond to HUPO-PSI defined terms
        // A complete list of these terms can be found here: https://github.com/HUPO-PSI/psi-ms-CV/blob/master/psi-ms.obo

        /// <summary>
        /// id: MS:1003057
        /// name: scan number
        /// def: "Ordinal number of the scan indicating its order of acquisition within a mass spectrometry acquisition run." [PSI: PI]
        /// is_a: MS:1000503 ! scan attribute
        /// </summary>
        [Required]
        public int ScanNumber { get; protected set; }
        /// <summary>
        /// id: MS:1000525
        /// name: spectrum representation
        /// comment: can take 2 possible values: Centroid or Profile
        /// def: "Way in which the spectrum is represented, either with regularly spaced data points or with a list of centroided peaks." [PSI: MS]
        /// relationship: part_of MS:1000442 ! spectrum
        /// </summary>
        [Required]
        public SpectrumRepresentationType SpectrumRepresentation { get; protected set; }
        /// <summary>
        /// id: MS:1000559
        /// name: spectrum type
        /// def: Defines a spectrum as MS1, MSN, etc.
        /// relationship: part_of MS:1000442 ! spectrum
        /// </summary>
        [Required]
        public MassSpectrumType MassSpectrumType { get; protected set; }
        /// <summary>
        /// id: MS:1000511
        /// name: ms level
        /// def: "Stage number achieved in a multi stage mass spectrometry acquisition." [PSI: MS]
        /// Example: for a MassSpectrum with MassSpectrumType == MSN, MsLevel would most commonly be 2
        /// is_a: MS:1000499 ! spectrum attribute
        /// </summary>
        [Required]
        public int MsLevel { get; protected set; }
        /// <summary>
        /// id: MS:1000443
        /// name: mass analyzer type
        /// def: "Mass analyzer separates the ions according to their mass-to-charge ratio." [PSI: MS]
        /// relationship: part_of MS:1000451 ! mass analyzer
        /// </summary>
        [Required]
        public MassAnalyzerType MassAnalyzerType { get; protected set; }
        /// <summary>
        /// id: MS:1000294
        /// name: mass spectrum
        /// def: "A plot of the relative abundance of a beam or other collection of ions as a function of the mass-to-charge ratio (m/z)." [PSI: MS]
        /// is_a: MS:1000524 ! data file content
        /// is_a: MS:1000559 ! spectrum type
        /// </summary>
        // TODO: Figure out what to do here as object is not a valid type for a database column
        [NotMapped]
        public Object? MassSpectrum { get; protected set; }
        /// <summary>
        /// id: MS:1000465
        /// name: scan polarity
        /// def: "Relative orientation of the electromagnetic field during the selection and detection of ions in the mass spectrometer." [PSI: MS]
        /// relationship: part_of MS:1000441 ! scan
        /// </summary>
        [Required]
        public ScanPolarityType ScanPolarity { get; protected set; }
        /// <summary>
        /// id: MS:1000016
        /// name: scan start time
        /// def: "The time that an analyzer started a scan, relative to the start of the MS run." [PSI: MS]
        /// is_a: MS:1000503 ! scan attribute
        /// is_a: MS:1002345 ! PSM-level attribute
        /// relationship: has_units UO:0000010 ! second
        /// relationship: has_units UO:0000031 ! minute
        /// relationship: has_value_type xsd:float ! The allowed value-type for this CV term
        /// </summary>
        public float? ScanStartTime { get; protected set; }
        /// <summary>
        /// id: MS:1000500
        /// name: scan window upper limit
        /// def: "The upper m/z bound of a mass spectrometer scan window." [PSI: MS]
        /// synonym: "mzRangeStop" RELATED[]
        /// is_a: MS:1000549 ! selection window attribute
        /// relationship: has_units MS:1000040 ! m/z
        /// </summary>
        public float? ScanWindowUpperLimit { get; protected set; }
        /// <summary>
        /// id: MS:1000501re
        /// name: scan window lower limit
        /// def: "The lower m/z bound of a mass spectrometer scan window." [PSI: MS]
        /// synonym: "mzRangeStop" RELATED[]
        /// is_a: MS:1000549 ! selection window attribute
        /// relationship: has_units MS:1000040 ! m/z
        /// </summary>
        public float? ScanWindowLowerLimit { get; protected set; }
        /// <summary>
        /// id: MS:1000512
        /// name: filter string
        /// def: "A string unique to Thermo instrument describing instrument settings for the scan." [PSI: MS]
        /// is_a: MS:1000503 ! scan attribute
        /// </summary>
        public string? FilterString { get; protected set; }
        /// <summary>
        /// id: MS:1000285
        /// name: total ion current
        /// def: "The sum of all the separate ion currents carried by the ions of different m/z contributing to a complete mass spectrum or in a specified m/z range of a mass spectrum." [PSI: MS]
        /// synonym: "TIC" EXACT[]
        /// is_a: MS:1003058 ! spectrum property
        /// </summary>
        public float? TotalIonCurrent { get; protected set; }
        /// <summary>
        /// id: MS:1000927
        /// name: ion injection time
        /// def: "The length of time spent filling an ion trapping device." [PSI: MS]
        /// is_a: MS:1000503 ! scan attribute
        /// relationship: has_units UO:0000028 ! millisecond
        /// </summary>
        public float? IonInjectionTime { get; protected set; }
        /// <summary>
        /// Non PSI-MS Field
        /// Gives the one based scan number of the scan where the isolated precursor was identified. 
        /// If the scan is an MS1Scan, PrecursorScanNumber is equal to -1
        /// </summary>
        public int? PrecursorScanNumber { get; protected set; }
        /// <summary>
        /// id: MS:1000744
        /// name: selected ion m/z
        /// def: "Mass-to-charge ratio of an selected ion." [PSI: MS]
        /// is_a: MS:1000455 ! ion selection attribute
        /// relationship: has_units MS:1000040 ! m/z
        /// </summary>
        public float? SelectedIonMz { get; protected set; }
        /// <summary>
        /// id: MS:1003208
        /// name: experimental precursor monoisotopic m/z
        /// def: "The measured or inferred m/z (as reported by the mass spectrometer acquisition software or post-processing software) of the monoisotopic peak of the precursor ion based on the MSn-1 spectrum." [PSI: MS]
        /// is_a: MS:1000455 ! ion selection attribute
        /// is_a: MS:1003295 ! summary statistics of replicates
        /// relationship: has_units MS:1000040 ! m/z
        /// </summary>
        public float? ExperimentalPrecursorMonoisotopicMz { get; protected set; }
        /// <summary>
        /// id: MS:1000827
        /// name: isolation window target m/z
        /// def: "The primary or reference m/z about which the isolation window is defined." [PSI: MS]
        /// is_a: MS:1000792 ! isolation window attribute
        /// relationship: has_units MS:1000040 ! m/z
        /// </summary>
        public float? IsolationWindowTargetMz { get; protected set; }
        /// <summary>
        /// id: MS:1000828
        /// name: isolation window lower offset
        /// def: "The extent of the isolation window in m/z below the isolation window target m/z. The lower and upper offsets may be asymmetric about the target m/z." [PSI: MS]
        /// is_a: MS:1000792 ! isolation window attribute
        /// relationship: has_units MS:1000040 ! m/z
        /// </summary>
        public float? IsolationWindowLowerOffset { get; protected set; }
        /// <summary>
        /// id: MS:1000828
        /// name: isolation window lower offset
        /// def: "The extent of the isolation window in m/z above the isolation window target m/z. The lower and upper offsets may be asymmetric about the target m/z." [PSI: MS]
        /// is_a: MS:1000792 ! isolation window attribute
        /// relationship: has_units MS:1000040 ! m/z
        /// </summary>
        public float? IsolationWindowUpperOffset { get; protected set; }
        /// <summary>
        /// id: MS:1000044
        /// name: dissociation method
        /// def: "Fragmentation method used for dissociation or fragmentation." [PSI: MS]
        /// synonym: "Activation Method" RELATED[]
        /// relationship: part_of MS:1000456 ! precursor activation
        /// </summary>
        public DissociationMethodType? DissociationMethod { get; protected set; }
        /// <summary>
        /// id: MS:1000138
        /// name: normalized collision energy
        /// def: "Instrument setting, expressed in percent, for adjusting collisional energies of ions in an effort to provide equivalent excitation of all ions." [PSI: PI]
        /// is_a: MS:1000510 ! precursor activation attribute
        /// relationship: has_units UO:0000187 ! percent
        /// </summary>
        public float? NormalizedCollisionEnergy { get; protected set; }

        #endregion

        #region NonPSIProperties
        // These properties don't map one-to-one to a PSI defined term.
        // However, anyone who can create a property to store the same information using the PSI-vocabulary
        // is encouraged to do so
        /// <summary>
        /// The instrument's best guess for the charge state of the selected precursor ion
        ///  Possibly related to PSI term "ion selection attribute" (MS:1000455)
        /// </summary>
        public int? SelectedIonChargeStateGuess { get; protected set; }
        /// <summary>
        /// The intensity of the selected precursor ion
        ///  Possibly related to PSI term "ion selection attribute" (MS:1000455)
        /// </summary>
        public float? SelectedIonIntensity { get; protected set; }
        /// <summary>
        /// TODO: Define the NativeID field. I'm not confident I know what it is
        /// Possibly related to PSI term "nativeID format" (MS:1000767)
        /// </summary>
        public string? NativeId { get; protected set; }
        #endregion

        public MsDataScanModel()
        {

        }

        public static MsDataScanModel GetModelFromMsDataScan(MsDataScan scan)
        {
            return new MsDataScanModel()
            {
                MassSpectrum = scan.MassSpectrum,
                ScanNumber = scan.OneBasedScanNumber,
                MsLevel = scan.MsnOrder,
                MassSpectrumType = scan.MsnOrder == 1 ? MassSpectrumType.MS1Spectrum : MassSpectrumType.MSnSpectrum, // MsDataScans don't distinguish between DDA, SIM, DIA, SRM, etc., so most MassSpectrumTypes can't be determined here. 
                SpectrumRepresentation = scan.IsCentroid ? SpectrumRepresentationType.Centroid : SpectrumRepresentationType.Profile,
                ScanPolarity = scan.Polarity == MassSpectrometry.Polarity.Negative ? ScanPolarityType.NegativeScan : ScanPolarityType.PositiveScan, // defaults to positive scan mode
                ScanStartTime = (float)scan.RetentionTime,
                ScanWindowLowerLimit = (float)scan.ScanWindowRange.Minimum,
                ScanWindowUpperLimit = (float)scan.ScanWindowRange.Maximum,
                FilterString = scan.ScanFilter,
                MassAnalyzerType = scan.MzAnalyzer.ToMassAnalyzerType(), 
                TotalIonCurrent = (float)scan.TotalIonCurrent,
                IonInjectionTime = (float?)scan.InjectionTime,
                NativeId = scan.NativeId,
                SelectedIonMz = (float?)scan.SelectedIonMZ,
                SelectedIonChargeStateGuess = scan.SelectedIonChargeStateGuess,
                SelectedIonIntensity = (float?)scan.SelectedIonIntensity,
                ExperimentalPrecursorMonoisotopicMz = (float?)scan.SelectedIonMonoisotopicGuessMz,
                IsolationWindowTargetMz = (float?)scan.IsolationMz,
                IsolationWindowUpperOffset = (float?)(scan.IsolationWidth / 2),
                IsolationWindowLowerOffset = (float?)(-1 * scan.IsolationWidth / 2),
                DissociationMethod = scan.DissociationType.ToDissociationMethodType(),
                PrecursorScanNumber = (int?)scan.OneBasedPrecursorScanNumber,
                NormalizedCollisionEnergy = !double.TryParse(scan.ScanDescription, out var collisionEnergy) ? null : (float)collisionEnergy
            };
        }
    }
}
