using RestAPI.Data.VO;

namespace RestAPI.Services
{
    public interface IFileService
    {
        public byte[] GetFile(string fileName);
        public Task<FileDetailVO> SaveFileToDisk(IFormFile file);
        public List<Task<FileDetailVO>> SaveFilesToDisk(IList<IFormFile> file);
    }
}
