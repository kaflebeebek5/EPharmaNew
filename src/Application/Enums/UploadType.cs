using System.ComponentModel;

namespace EPharma.Application.Enums
{
    public enum UploadType : byte
    {
        [Description(@"Images\ProfilePictures")]
        ProfilePicture,
        [Description(@"Photo")]
        Picture,
        [Description(@"EPharmaPhotos")]
        EPharmaPhotos,
        [Description(@"DoctorPhoto")]
        DoctorPhoto 
    }
}