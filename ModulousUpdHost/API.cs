using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using System.IO;

namespace ModulousLib.Web
{
    public class ModPage
    {
        //GET /api/browse?page=<integer>&orderby=<string>&order=<string>&count=<integer>
        public int page { get; set; }
        public int pages { get; set; }
        public int count { get; set; }
        public List<OnlineMod> result { get; set; }

        public static ModPage browse_mods_from_api(int page = 1, string orderby = "created", string order = "asc",
            int count = 30)
        {
            using (var client = new WebClient())
            {
                var s =
                    client.DownloadString(new Uri(new Uri(Globals.site_url),
                        "/api/browse_manager?page=" + page + "&orderby=" + orderby + "&order=" + order + "&count=" +
                        count));
                var result = JsonConvert.DeserializeObject<ModPage>(s);
                result.result = result.result.OrderBy(mod => mod.id).ToList();
                return result;
            }
        }
    }

    public class VersionInfo
    {
        public float version { get; set; }
        public string version_path { get; set; }

        public static VersionInfo get_version_info_from_api()
        {
                using (var client = new WebClient())
                {
                    var s =
                        client.DownloadString(new Uri(new Uri(Globals.site_url),
                            "/api/modmm/updates"));
                    var result = JsonConvert.DeserializeObject<VersionInfo>(s);
                    return result;
                }
        }

        public static VersionInfo FromFile(string file)
        {
            var file_contents = File.ReadAllText(file);
            return JsonConvert.DeserializeObject<VersionInfo>(file_contents);
        }
    }
}