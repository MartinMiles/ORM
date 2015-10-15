using System;

namespace ORM.Test
{
    public class Model : BaseModel
    {
        [Field("Id")]
        public int Id { get; set; }

        [Field("Int")]
        public int? Int { get; set; }

        [Field("Nvarchar255")]
        public string Nvarchar255 { get; set; }
        
        [Field("NvarcharMax")]
        public string NvarcharMax { get; set; }

        [Field("Ntext")]
        public string Ntext { get; set; }

        [Field("DateTime")]
        public DateTime? DateTime { get; set; }

        [Field("Money")]
        public decimal? Money { get; set; }
        
        [Field("Bit")]
        public bool? Bit { get; set; }
    }

/*
    public class Model : BaseModel
    {
        [Field("artArticleID")]
        public int ArtArticleId { get; set; }

        [Field("artIssueID")]
        public int ArtIssueId { get; set; }

        [Field("artTitle")]
        public string ArtTitle { get; set; }

        [Field("artDescription")]
        public string ArtDescription { get; set; }

        [Field("artBody")]
        public string ArtBody { get; set; }

        [Field("artActive")]
        public bool ArtActive { get; set; }

        [Field("artActiveDate")]
        public DateTime ArtActiveDate { get; set; }

        [Field("artStoryID")]
        public int? ArtStoryId { get; set; }

        [Field("artImportXML")]
        public string ArtImportXml { get; set; }

        [Field("artPictureLarge")]
        public string ArtPictureLarge { get; set; }

        [Field("artPictureSmall")]
        public string ArtPictureSmall { get; set; }

        [Field("artPictureCaption")]
        public string ArtPictureCaption { get; set; }

        [Field("artFree")]
        public bool ArtFree { get; set; }

        [Field("artExclusive")]
        public bool ArtExclusive { get; set; }

        [Field("artPopup")]
        public bool ArtPopup { get; set; }

        [Field("artMaxWords")]
        public int? ArtMaxWords { get; set; }

        [Field("artMinWords")]
        public int? ArtMinWords { get; set; }

        [Field("artMPUPosition")]
        public int? ArtMpuPosition { get; set; }

        [Field("artISISourceID")]
        public int? ArtIsiSourceId { get; set; }

        [Field("artUpdateDate")]
        public DateTime ArtUpdateDate { get; set; }


        private DateTime? _artLastGoogle;

        [Field("artLastGoogle")]
        public DateTime? ArtLastGoogle
        {
            get { return _artLastGoogle; }
            set
            {
                _artLastGoogle = value;

                if (value != null)
                {
                    if (value == DateTime.MinValue)
                    {
                        _artLastGoogle = null;
                    }
                }

            }
        }

        [Field("artAdTag")]
        public string ArtAdTag { get; set; }

        [Field("artKeywords")]
        public string ArtKeywords { get; set; }

        [Field("artDownload")]
        public string ArtDownload { get; set; }

        [Field("artSubHeadline")]
        public string ArtSubHeadline { get; set; }

        [Field("artRank")]
        public int? ArtRank { get; set; }

        [Field("artFlag1")]
        public bool? ArtFlag1 { get; set; }

        [Field("artSharedDownload")]
        public string ArtSharedDownload { get; set; }


        private DateTime? _artExpiryDate;

        [Field("artExpiryDate")]
        public DateTime? ArtExpiryDate
        {
            get { return _artExpiryDate; }
            set
            {
                _artExpiryDate = value;

                if (value != null)
                {
                    if (value == DateTime.MinValue)
                    {
                        _artExpiryDate = null;
                    }
                }

            }
        }

        [Field("artLink")]
        public string ArtLink { get; set; }

        [Field("artLanguageID")]
        public int? ArtLanguageId { get; set; }

        [Field("artWordCount")]
        public int? ArtWordCount { get; set; }

        [Field("artStagedPDF")]
        public string ArtStagedPdf { get; set; }

        [Field("artRequiredSubscriberLevelID")]
        public int? ArtRequiredSubscriberLevelId { get; set; }

        [Field("artRequiredStatusMask")]
        public int? ArtRequiredStatusMask { get; set; }

        [Field("artPrintDisabled")]
        public bool? ArtPrintDisabled { get; set; }

        [Field("artSpiked")]
        public bool? ArtSpiked { get; set; }

        [Field("artWorkflowed")]
        public int? ArtWorkflowed { get; set; }

        private DateTime? _artCreatedDate;
        [Field("artCreatedDate")]
        public DateTime? ArtCreatedDate
        {
            get { return _artCreatedDate; }
            set
            {
                _artCreatedDate = value;

                if (value != null)
                {
                    if (value == DateTime.MinValue)
                    {
                        _artCreatedDate = null;
                    }
                }

            }
        }

    }
*/
}
