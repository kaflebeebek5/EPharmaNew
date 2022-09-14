using AutoMapper;
using EPharma.Application.Extensions;
using EPharma.Application.Features.StaticVariable.Queries.GetByName;
using EPharma.Application.Interfaces.Services;
using EPharma.Application.Requests;
using EPharma.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EPharma.Infrastructure.Services
{  
    public class UploadService : IUploadService
    {
        private readonly EPharmaContext _db;

        public UploadService(
        EPharmaContext db)
        {
            _db = db;
        }
        public string UploadAsync(UploadRequest request)
        {
            if (request.Data == null) return string.Empty;
            var streamData = new MemoryStream(request.Data);
            if (streamData.Length > 0)
            {
                var FolderPath = _db.StaticVariable.Where(x => x.Name == "ImagePath").Select(x => x.Value).FirstOrDefault();
                var folder = request.UploadType.ToDescriptionString();
                var folderName = Path.Combine(FolderPath, folder);
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                bool exists = System.IO.Directory.Exists(pathToSave);
                if (!exists)
                    System.IO.Directory.CreateDirectory(pathToSave);
                var fileName = request.FileName.Trim('"');
                var fullPath = Path.Combine(pathToSave, fileName);
                var dbPath = Path.Combine(folderName, fileName);
                var ReturnPath = Path.Combine(folder, fileName);
                if (File.Exists(dbPath))
                {
                    dbPath = NextAvailableFilename(dbPath);
                    fullPath = NextAvailableFilename(fullPath);
                }
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    streamData.CopyTo(stream);
                }
                return ReturnPath;
            }
            else
            {
                return string.Empty;
            }
            //if (request.Data == null) return string.Empty;
            //var streamData = new MemoryStream(request.Data);
            //if (streamData.Length > 0)
            //{
            //    var folderpath =  _db.StaticVariable.Where(x => x.Name == "ImagePath").Select(s => s.Value).FirstOrDefault();
            //    var folder = request.UploadType.ToDescriptionString();
            //    var folderName = Path.Combine(folderpath, folder);
            //    var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            //    bool exists = System.IO.Directory.Exists(pathToSave);
            //    if (!exists)
            //        System.IO.Directory.CreateDirectory(pathToSave);
            //    var fileName = request.FileName.Trim('"');
            //    var fullPath = Path.Combine(pathToSave, fileName);
            //    var dbPath = Path.Combine(folderName, fileName);
            //    if (File.Exists(dbPath))
            //    {
            //        dbPath = NextAvailableFilename(dbPath);
            //        fullPath = NextAvailableFilename(fullPath);
            //    }
            //    using (var stream = new FileStream(fullPath, FileMode.Create))
            //    {
            //        streamData.CopyTo(stream);
            //    }
            //    return dbPath;
            //}
            //else
            //{
            //    return string.Empty;
            //}
        }

        private static string numberPattern = " ({0})";

        public static string NextAvailableFilename(string path)
        {
            // Short-cut if already available
            if (!File.Exists(path))
                return path;

            // If path has extension then insert the number pattern just before the extension and return next filename
            if (Path.HasExtension(path))
                return GetNextFilename(path.Insert(path.LastIndexOf(Path.GetExtension(path)), numberPattern));

            // Otherwise just append the pattern to the path and return next filename
            return GetNextFilename(path + numberPattern);
        }

        private static string GetNextFilename(string pattern)
        {
            string tmp = string.Format(pattern, 1);
            //if (tmp == pattern)
            //throw new ArgumentException("The pattern must include an index place-holder", "pattern");

            if (!File.Exists(tmp))
                return tmp; // short-circuit if no matches

            int min = 1, max = 2; // min is inclusive, max is exclusive/untested

            while (File.Exists(string.Format(pattern, max)))
            {
                min = max;
                max *= 2;
            }

            while (max != min + 1)
            {
                int pivot = (max + min) / 2;
                if (File.Exists(string.Format(pattern, pivot)))
                    min = pivot;
                else
                    max = pivot;
            }

            return string.Format(pattern, max);
        }
    }
}