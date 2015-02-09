using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Collections.Specialized;
using System.IO;
using System.Net;

namespace SOS.Lib.Util
{
	public class WebUtility
	{
		#region Constants

		public const string DefaultMimeType = "application/octet-stream";

		#endregion

		#region Properties

		#region Static

		private static readonly Dictionary<string, string> _mimeTypesByFileExtension;

		public static Dictionary<string, string> MimeTypesByFileExtension
		{
			get { return _mimeTypesByFileExtension; }
		}

		#endregion Static

		#endregion Properties

		#region Constructors

		static WebUtility()
		{
			_mimeTypesByFileExtension = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);

			#region Build Mime Type List

			_mimeTypesByFileExtension.Add(".3dm", "x-world/x-3dmf");
			_mimeTypesByFileExtension.Add(".3dmf", "x-world/x-3dmf");
			_mimeTypesByFileExtension.Add(".a", "application/octet-stream");
			_mimeTypesByFileExtension.Add(".aab", "application/x-authorware-bin");
			_mimeTypesByFileExtension.Add(".aam", "application/x-authorware-map");
			_mimeTypesByFileExtension.Add(".aas", "application/x-authorware-seg");
			_mimeTypesByFileExtension.Add(".abc", "text/vnd.abc");
			_mimeTypesByFileExtension.Add(".acgi", "text/html");
			_mimeTypesByFileExtension.Add(".afl", "video/animaflex");
			_mimeTypesByFileExtension.Add(".ai", "application/postscript");
			_mimeTypesByFileExtension.Add(".aif", "audio/aiff");
			_mimeTypesByFileExtension.Add(".aifc", "audio/aiff");
			_mimeTypesByFileExtension.Add(".aiff", "audio/aiff");
			_mimeTypesByFileExtension.Add(".aim", "application/x-aim");
			_mimeTypesByFileExtension.Add(".aip", "text/x-audiosoft-intra");
			_mimeTypesByFileExtension.Add(".ani", "application/x-navi-animation");
			_mimeTypesByFileExtension.Add(".aos", "application/x-nokia-9000-communicator-add-on-software");
			_mimeTypesByFileExtension.Add(".aps", "application/mime");
			_mimeTypesByFileExtension.Add(".arc", "application/octet-stream");
			_mimeTypesByFileExtension.Add(".arj", "application/arj");
			_mimeTypesByFileExtension.Add(".art", "image/x-jg");
			_mimeTypesByFileExtension.Add(".asf", "video/x-ms-asf");
			_mimeTypesByFileExtension.Add(".asm", "text/x-asm");
			_mimeTypesByFileExtension.Add(".asp", "text/asp");
			_mimeTypesByFileExtension.Add(".asx", "video/x-ms-asf");
			_mimeTypesByFileExtension.Add(".au", "audio/x-au");
			_mimeTypesByFileExtension.Add(".avi", "video/avi");
			_mimeTypesByFileExtension.Add(".avs", "video/avs-video");
			_mimeTypesByFileExtension.Add(".bcpio", "application/x-bcpio");
			_mimeTypesByFileExtension.Add(".bin", "application/octet-stream");
			_mimeTypesByFileExtension.Add(".bm", "image/bmp");
			_mimeTypesByFileExtension.Add(".bmp", "image/bmp");
			_mimeTypesByFileExtension.Add(".boo", "application/book");
			_mimeTypesByFileExtension.Add(".book", "application/book");
			_mimeTypesByFileExtension.Add(".boz", "application/x-bzip2");
			_mimeTypesByFileExtension.Add(".bsh", "application/x-bsh");
			_mimeTypesByFileExtension.Add(".bz", "application/x-bzip");
			_mimeTypesByFileExtension.Add(".bz2", "application/x-bzip2");
			_mimeTypesByFileExtension.Add(".c", "text/plain");
			_mimeTypesByFileExtension.Add(".c++", "text/plain");
			_mimeTypesByFileExtension.Add(".cat", "application/vnd.ms-pki.seccat");
			_mimeTypesByFileExtension.Add(".cc", "text/plain");
			_mimeTypesByFileExtension.Add(".ccad", "application/clariscad");
			_mimeTypesByFileExtension.Add(".cco", "application/x-cocoa");
			_mimeTypesByFileExtension.Add(".cdf", "application/cdf");
			_mimeTypesByFileExtension.Add(".cer", "application/x-x509-ca-cert");
			_mimeTypesByFileExtension.Add(".cha", "application/x-chat");
			_mimeTypesByFileExtension.Add(".chat", "application/x-chat");
			_mimeTypesByFileExtension.Add(".class", "application/java");
			_mimeTypesByFileExtension.Add(".com", "application/octet-stream");
			_mimeTypesByFileExtension.Add(".conf", "text/plain");
			_mimeTypesByFileExtension.Add(".cpio", "application/x-cpio");
			_mimeTypesByFileExtension.Add(".cpp", "text/x-c");
			_mimeTypesByFileExtension.Add(".cpt", "application/x-cpt");
			_mimeTypesByFileExtension.Add(".crl", "application/pkcs-crl");
			_mimeTypesByFileExtension.Add(".crt", "application/x-x509-ca-cert");
			_mimeTypesByFileExtension.Add(".csh", "application/x-csh");
			_mimeTypesByFileExtension.Add(".css", "text/css");
			_mimeTypesByFileExtension.Add(".cxx", "text/plain");
			_mimeTypesByFileExtension.Add(".dcr", "application/x-director");
			_mimeTypesByFileExtension.Add(".deepv", "application/x-deepv");
			_mimeTypesByFileExtension.Add(".def", "text/plain");
			_mimeTypesByFileExtension.Add(".der", "application/x-x509-ca-cert");
			_mimeTypesByFileExtension.Add(".dif", "video/x-dv");
			_mimeTypesByFileExtension.Add(".dir", "application/x-director");
			_mimeTypesByFileExtension.Add(".dl", "video/dl");
			_mimeTypesByFileExtension.Add(".doc", "application/msword");
			_mimeTypesByFileExtension.Add(".dot", "application/msword");
			_mimeTypesByFileExtension.Add(".dp", "application/commonground");
			_mimeTypesByFileExtension.Add(".drw", "application/drafting");
			_mimeTypesByFileExtension.Add(".dump", "application/octet-stream");
			_mimeTypesByFileExtension.Add(".dv", "video/x-dv");
			_mimeTypesByFileExtension.Add(".dvi", "application/x-dvi");
			_mimeTypesByFileExtension.Add(".dwf", "model/vnd.dwf");
			_mimeTypesByFileExtension.Add(".dwg", "image/x-dwg");
			_mimeTypesByFileExtension.Add(".dxf", "image/x-dwg");
			_mimeTypesByFileExtension.Add(".dxr", "application/x-director");
			_mimeTypesByFileExtension.Add(".el", "text/x-script.elisp");
			_mimeTypesByFileExtension.Add(".elc", "application/x-elc");
			_mimeTypesByFileExtension.Add(".env", "application/x-envoy");
			_mimeTypesByFileExtension.Add(".eps", "application/postscript");
			_mimeTypesByFileExtension.Add(".es", "application/x-esrehber");
			_mimeTypesByFileExtension.Add(".etx", "text/x-setext");
			_mimeTypesByFileExtension.Add(".evy", "application/envoy");
			_mimeTypesByFileExtension.Add(".exe", "application/octet-stream");
			_mimeTypesByFileExtension.Add(".f", "text/plain");
			_mimeTypesByFileExtension.Add(".f77", "text/x-fortran");
			_mimeTypesByFileExtension.Add(".f90", "text/plain");
			_mimeTypesByFileExtension.Add(".fdf", "application/vnd.fdf");
			_mimeTypesByFileExtension.Add(".fif", "image/fif");
			_mimeTypesByFileExtension.Add(".fli", "video/fli");
			_mimeTypesByFileExtension.Add(".flo", "image/florian");
			_mimeTypesByFileExtension.Add(".flx", "text/vnd.fmi.flexstor");
			_mimeTypesByFileExtension.Add(".fmf", "video/x-atomic3d-feature");
			_mimeTypesByFileExtension.Add(".for", "text/plain");
			_mimeTypesByFileExtension.Add(".fpx", "image/vnd.fpx");
			_mimeTypesByFileExtension.Add(".frl", "application/freeloader");
			_mimeTypesByFileExtension.Add(".funk", "audio/make");
			_mimeTypesByFileExtension.Add(".g", "text/plain");
			_mimeTypesByFileExtension.Add(".g3", "image/g3fax");
			_mimeTypesByFileExtension.Add(".gif", "image/gif");
			_mimeTypesByFileExtension.Add(".gl", "video/gl");
			_mimeTypesByFileExtension.Add(".gsd", "audio/x-gsm");
			_mimeTypesByFileExtension.Add(".gsm", "audio/x-gsm");
			_mimeTypesByFileExtension.Add(".gsp", "application/x-gsp");
			_mimeTypesByFileExtension.Add(".gss", "application/x-gss");
			_mimeTypesByFileExtension.Add(".gtar", "application/x-gtar");
			_mimeTypesByFileExtension.Add(".gz", "application/x-gzip");
			_mimeTypesByFileExtension.Add(".gzip", "application/x-gzip");
			_mimeTypesByFileExtension.Add(".h", "text/plain");
			_mimeTypesByFileExtension.Add(".hdf", "application/x-hdf");
			_mimeTypesByFileExtension.Add(".help", "application/x-helpfile");
			_mimeTypesByFileExtension.Add(".hgl", "application/vnd.hp-hpgl");
			_mimeTypesByFileExtension.Add(".hh", "text/plain");
			_mimeTypesByFileExtension.Add(".hlb", "text/x-script");
			_mimeTypesByFileExtension.Add(".hlp", "application/hlp");
			_mimeTypesByFileExtension.Add(".hpg", "application/vnd.hp-hpgl");
			_mimeTypesByFileExtension.Add(".hpgl", "application/vnd.hp-hpgl");
			_mimeTypesByFileExtension.Add(".hqx", "application/binhex");
			_mimeTypesByFileExtension.Add(".hta", "application/hta");
			_mimeTypesByFileExtension.Add(".htc", "text/x-component");
			_mimeTypesByFileExtension.Add(".htm", "text/html");
			_mimeTypesByFileExtension.Add(".html", "text/html");
			_mimeTypesByFileExtension.Add(".htmls", "text/html");
			_mimeTypesByFileExtension.Add(".htt", "text/webviewhtml");
			_mimeTypesByFileExtension.Add(".htx", "text/html");
			_mimeTypesByFileExtension.Add(".ice", "x-conference/x-cooltalk");
			_mimeTypesByFileExtension.Add(".ico", "image/x-icon");
			_mimeTypesByFileExtension.Add(".idc", "text/plain");
			_mimeTypesByFileExtension.Add(".ief", "image/ief");
			_mimeTypesByFileExtension.Add(".iefs", "image/ief");
			_mimeTypesByFileExtension.Add(".iges", "application/iges");
			_mimeTypesByFileExtension.Add(".igs", "application/iges");
			_mimeTypesByFileExtension.Add(".ima", "application/x-ima");
			_mimeTypesByFileExtension.Add(".imap", "application/x-httpd-imap");
			_mimeTypesByFileExtension.Add(".inf", "application/inf");
			_mimeTypesByFileExtension.Add(".ins", "application/x-internett-signup");
			_mimeTypesByFileExtension.Add(".ip", "application/x-ip2");
			_mimeTypesByFileExtension.Add(".isu", "video/x-isvideo");
			_mimeTypesByFileExtension.Add(".it", "audio/it");
			_mimeTypesByFileExtension.Add(".iv", "application/x-inventor");
			_mimeTypesByFileExtension.Add(".ivr", "i-world/i-vrml");
			_mimeTypesByFileExtension.Add(".ivy", "application/x-livescreen");
			_mimeTypesByFileExtension.Add(".jam", "audio/x-jam");
			_mimeTypesByFileExtension.Add(".jav", "text/plain");
			_mimeTypesByFileExtension.Add(".java", "text/plain");
			_mimeTypesByFileExtension.Add(".jcm", "application/x-java-commerce");
			_mimeTypesByFileExtension.Add(".jfif", "image/jpeg");
			_mimeTypesByFileExtension.Add(".jfif-tbnl", "image/jpeg");
			_mimeTypesByFileExtension.Add(".jpe", "image/jpeg");
			_mimeTypesByFileExtension.Add(".jpeg", "image/jpeg");
			_mimeTypesByFileExtension.Add(".jpg", "image/jpeg");
			_mimeTypesByFileExtension.Add(".jps", "image/x-jps");
			_mimeTypesByFileExtension.Add(".js", "application/x-javascript");
			_mimeTypesByFileExtension.Add(".jut", "image/jutvision");
			_mimeTypesByFileExtension.Add(".kar", "audio/midi");
			_mimeTypesByFileExtension.Add(".ksh", "application/x-ksh");
			_mimeTypesByFileExtension.Add(".la", "audio/nspaudio");
			_mimeTypesByFileExtension.Add(".lam", "audio/x-liveaudio");
			_mimeTypesByFileExtension.Add(".latex", "application/x-latex");
			_mimeTypesByFileExtension.Add(".lha", "application/lha");
			_mimeTypesByFileExtension.Add(".lhx", "application/octet-stream");
			_mimeTypesByFileExtension.Add(".list", "text/plain");
			_mimeTypesByFileExtension.Add(".lma", "audio/nspaudio");
			_mimeTypesByFileExtension.Add(".log", "text/plain");
			_mimeTypesByFileExtension.Add(".lsp", "application/x-lisp");
			_mimeTypesByFileExtension.Add(".lst", "text/plain");
			_mimeTypesByFileExtension.Add(".lsx", "text/x-la-asf");
			_mimeTypesByFileExtension.Add(".ltx", "application/x-latex");
			_mimeTypesByFileExtension.Add(".lzh", "application/octet-stream");
			_mimeTypesByFileExtension.Add(".lzx", "application/lzx");
			_mimeTypesByFileExtension.Add(".m", "text/plain");
			_mimeTypesByFileExtension.Add(".m1v", "video/mpeg");
			_mimeTypesByFileExtension.Add(".m2a", "audio/mpeg");
			_mimeTypesByFileExtension.Add(".m2v", "video/mpeg");
			_mimeTypesByFileExtension.Add(".m3u", "audio/x-mpequrl");
			_mimeTypesByFileExtension.Add(".man", "application/x-troff-man");
			_mimeTypesByFileExtension.Add(".map", "application/x-navimap");
			_mimeTypesByFileExtension.Add(".mar", "text/plain");
			_mimeTypesByFileExtension.Add(".mbd", "application/mbedlet");
			_mimeTypesByFileExtension.Add(".mc$", "application/x-magic-cap-package-1.0");
			_mimeTypesByFileExtension.Add(".mcd", "application/mcad");
			_mimeTypesByFileExtension.Add(".mcf", "image/vasa");
			_mimeTypesByFileExtension.Add(".mcp", "application/netmc");
			_mimeTypesByFileExtension.Add(".me", "application/x-troff-me");
			_mimeTypesByFileExtension.Add(".mht", "message/rfc822");
			_mimeTypesByFileExtension.Add(".mhtml", "message/rfc822");
			_mimeTypesByFileExtension.Add(".mid", "audio/x-mid");
			_mimeTypesByFileExtension.Add(".midi", "audio/x-mid");
			_mimeTypesByFileExtension.Add(".mif", "application/x-mif");
			_mimeTypesByFileExtension.Add(".mime", "message/rfc822");
			_mimeTypesByFileExtension.Add(".mjf", "audio/x-vnd.audioexplosion.mjuicemediafile");
			_mimeTypesByFileExtension.Add(".mjpg", "video/x-motion-jpeg");
			_mimeTypesByFileExtension.Add(".mm", "application/x-meme");
			_mimeTypesByFileExtension.Add(".mme", "application/base64");
			_mimeTypesByFileExtension.Add(".mod", "audio/x-mod");
			_mimeTypesByFileExtension.Add(".moov", "video/quicktime");
			_mimeTypesByFileExtension.Add(".mov", "video/quicktime");
			_mimeTypesByFileExtension.Add(".movie", "video/x-sgi-movie");
			_mimeTypesByFileExtension.Add(".mp2", "video/x-mpeg");
			_mimeTypesByFileExtension.Add(".mp3", "video/x-mpeg");
			_mimeTypesByFileExtension.Add(".mpa", "video/mpeg");
			_mimeTypesByFileExtension.Add(".mpc", "application/x-project");
			_mimeTypesByFileExtension.Add(".mpe", "video/mpeg");
			_mimeTypesByFileExtension.Add(".mpeg", "video/mpeg");
			_mimeTypesByFileExtension.Add(".mpg", "video/mpeg");
			_mimeTypesByFileExtension.Add(".mpga", "audio/mpeg");
			_mimeTypesByFileExtension.Add(".mpp", "application/vnd.ms-project");
			_mimeTypesByFileExtension.Add(".mpt", "application/x-project");
			_mimeTypesByFileExtension.Add(".mpv", "application/x-project");
			_mimeTypesByFileExtension.Add(".mpx", "application/x-project");
			_mimeTypesByFileExtension.Add(".mrc", "application/marc");
			_mimeTypesByFileExtension.Add(".ms", "application/x-troff-ms");
			_mimeTypesByFileExtension.Add(".mv", "video/x-sgi-movie");
			_mimeTypesByFileExtension.Add(".my", "audio/make");
			_mimeTypesByFileExtension.Add(".mzz", "application/x-vnd.audioexplosion.mzz");
			_mimeTypesByFileExtension.Add(".nap", "image/naplps");
			_mimeTypesByFileExtension.Add(".naplps", "image/naplps");
			_mimeTypesByFileExtension.Add(".nc", "application/x-netcdf");
			_mimeTypesByFileExtension.Add(".ncm", "application/vnd.nokia.configuration-message");
			_mimeTypesByFileExtension.Add(".nif", "image/x-niff");
			_mimeTypesByFileExtension.Add(".niff", "image/x-niff");
			_mimeTypesByFileExtension.Add(".nix", "application/x-mix-transfer");
			_mimeTypesByFileExtension.Add(".nsc", "application/x-conference");
			_mimeTypesByFileExtension.Add(".nvd", "application/x-navidoc");
			_mimeTypesByFileExtension.Add(".o", "application/octet-stream");
			_mimeTypesByFileExtension.Add(".oda", "application/oda");
			_mimeTypesByFileExtension.Add(".omc", "application/x-omc");
			_mimeTypesByFileExtension.Add(".omcd", "application/x-omcdatamaker");
			_mimeTypesByFileExtension.Add(".omcr", "application/x-omcregerator");
			_mimeTypesByFileExtension.Add(".p", "text/x-pascal");
			_mimeTypesByFileExtension.Add(".p10", "application/x-pkcs10");
			_mimeTypesByFileExtension.Add(".p12", "application/x-pkcs12");
			_mimeTypesByFileExtension.Add(".p7a", "application/x-pkcs7-signature");
			_mimeTypesByFileExtension.Add(".p7c", "application/x-pkcs7-mime");
			_mimeTypesByFileExtension.Add(".p7m", "application/x-pkcs7-mime");
			_mimeTypesByFileExtension.Add(".p7r", "application/x-pkcs7-certreqresp");
			_mimeTypesByFileExtension.Add(".p7s", "application/pkcs7-signature");
			_mimeTypesByFileExtension.Add(".part", "application/pro_eng");
			_mimeTypesByFileExtension.Add(".pas", "text/pascal");
			_mimeTypesByFileExtension.Add(".pbm", "image/x-portable-bitmap");
			_mimeTypesByFileExtension.Add(".pcl", "application/x-pcl");
			_mimeTypesByFileExtension.Add(".pct", "image/x-pict");
			_mimeTypesByFileExtension.Add(".pcx", "image/x-pcx");
			_mimeTypesByFileExtension.Add(".pdb", "chemical/x-pdb");
			_mimeTypesByFileExtension.Add(".pdf", "application/pdf");
			_mimeTypesByFileExtension.Add(".pfunk", "audio/make");
			_mimeTypesByFileExtension.Add(".pgm", "image/x-portable-graymap");
			_mimeTypesByFileExtension.Add(".pic", "image/pict");
			_mimeTypesByFileExtension.Add(".pict", "image/pict");
			_mimeTypesByFileExtension.Add(".pkg", "application/x-newton-compatible-pkg");
			_mimeTypesByFileExtension.Add(".pko", "application/vnd.ms-pki.pko");
			_mimeTypesByFileExtension.Add(".pl", "text/plain");
			_mimeTypesByFileExtension.Add(".plx", "application/x-pixclscript");
			_mimeTypesByFileExtension.Add(".pm", "image/x-xpixmap");
			_mimeTypesByFileExtension.Add(".pm4", "application/x-pagemaker");
			_mimeTypesByFileExtension.Add(".pm5", "application/x-pagemaker");
			_mimeTypesByFileExtension.Add(".png", "image/png");
			_mimeTypesByFileExtension.Add(".pnm", "image/x-portable-anymap");
			_mimeTypesByFileExtension.Add(".pot", "application/mspowerpoint");
			_mimeTypesByFileExtension.Add(".pov", "model/x-pov");
			_mimeTypesByFileExtension.Add(".ppa", "application/vnd.ms-powerpoint");
			_mimeTypesByFileExtension.Add(".ppm", "image/x-portable-pixmap");
			_mimeTypesByFileExtension.Add(".pps", "application/mspowerpoint");
			_mimeTypesByFileExtension.Add(".ppt", "application/mspowerpoint");
			_mimeTypesByFileExtension.Add(".ppz", "application/mspowerpoint");
			_mimeTypesByFileExtension.Add(".pre", "application/x-freelance");
			_mimeTypesByFileExtension.Add(".prt", "application/pro_eng");
			_mimeTypesByFileExtension.Add(".ps", "application/postscript");
			_mimeTypesByFileExtension.Add(".psd", "application/octet-stream");
			_mimeTypesByFileExtension.Add(".pvu", "paleovu/x-pv");
			_mimeTypesByFileExtension.Add(".pwz", "application/vnd.ms-powerpoint");
			_mimeTypesByFileExtension.Add(".py", "text/x-script.phyton");
			_mimeTypesByFileExtension.Add(".pyc", "applicaiton/x-bytecode.python");
			_mimeTypesByFileExtension.Add(".qcp", "audio/vnd.qcelp");
			_mimeTypesByFileExtension.Add(".qd3", "x-world/x-3dmf");
			_mimeTypesByFileExtension.Add(".qd3d", "x-world/x-3dmf");
			_mimeTypesByFileExtension.Add(".qif", "image/x-quicktime");
			_mimeTypesByFileExtension.Add(".qt", "video/quicktime");
			_mimeTypesByFileExtension.Add(".qtc", "video/x-qtc");
			_mimeTypesByFileExtension.Add(".qti", "image/x-quicktime");
			_mimeTypesByFileExtension.Add(".qtif", "image/x-quicktime");
			_mimeTypesByFileExtension.Add(".ra", "audio/x-realaudio");
			_mimeTypesByFileExtension.Add(".ram", "audio/x-pn-realaudio");
			_mimeTypesByFileExtension.Add(".ras", "image/x-cmu-raster");
			_mimeTypesByFileExtension.Add(".rast", "image/cmu-raster");
			_mimeTypesByFileExtension.Add(".rexx", "text/x-script.rexx");
			_mimeTypesByFileExtension.Add(".rf", "image/vnd.rn-realflash");
			_mimeTypesByFileExtension.Add(".rgb", "image/x-rgb");
			_mimeTypesByFileExtension.Add(".rm", "audio/x-pn-realaudio");
			_mimeTypesByFileExtension.Add(".rmi", "audio/mid");
			_mimeTypesByFileExtension.Add(".rmm", "audio/x-pn-realaudio");
			_mimeTypesByFileExtension.Add(".rmp", "audio/x-pn-realaudio");
			_mimeTypesByFileExtension.Add(".rng", "application/vnd.nokia.ringing-tone");
			_mimeTypesByFileExtension.Add(".rnx", "application/vnd.rn-realplayer");
			_mimeTypesByFileExtension.Add(".roff", "application/x-troff");
			_mimeTypesByFileExtension.Add(".rp", "image/vnd.rn-realpix");
			_mimeTypesByFileExtension.Add(".rpm", "audio/x-pn-realaudio-plugin");
			_mimeTypesByFileExtension.Add(".rt", "text/richtext");
			_mimeTypesByFileExtension.Add(".rtf", "text/richtext");
			_mimeTypesByFileExtension.Add(".rtx", "text/richtext");
			_mimeTypesByFileExtension.Add(".rv", "video/vnd.rn-realvideo");
			_mimeTypesByFileExtension.Add(".s", "text/x-asm");
			_mimeTypesByFileExtension.Add(".s3m", "audio/s3m");
			_mimeTypesByFileExtension.Add(".saveme", "application/octet-stream");
			_mimeTypesByFileExtension.Add(".sbk", "application/x-tbook");
			_mimeTypesByFileExtension.Add(".scm", "video/x-scm");
			_mimeTypesByFileExtension.Add(".sdml", "text/plain");
			_mimeTypesByFileExtension.Add(".sdp", "application/x-sdp");
			_mimeTypesByFileExtension.Add(".sdr", "application/sounder");
			_mimeTypesByFileExtension.Add(".sea", "application/x-sea");
			_mimeTypesByFileExtension.Add(".set", "application/set");
			_mimeTypesByFileExtension.Add(".sgm", "text/sgml");
			_mimeTypesByFileExtension.Add(".sgml", "text/sgml");
			_mimeTypesByFileExtension.Add(".sh", "application/x-bsh");
			_mimeTypesByFileExtension.Add(".shar", "application/x-shar");
			_mimeTypesByFileExtension.Add(".shtml", "text/html");
			_mimeTypesByFileExtension.Add(".sid", "audio/x-psid");
			_mimeTypesByFileExtension.Add(".sit", "application/x-sit");
			_mimeTypesByFileExtension.Add(".skd", "application/x-koan");
			_mimeTypesByFileExtension.Add(".skm", "application/x-koan");
			_mimeTypesByFileExtension.Add(".skp", "application/x-koan");
			_mimeTypesByFileExtension.Add(".skt", "application/x-koan");
			_mimeTypesByFileExtension.Add(".sl", "application/x-seelogo");
			_mimeTypesByFileExtension.Add(".smi", "application/smil");
			_mimeTypesByFileExtension.Add(".smil", "application/smil");
			_mimeTypesByFileExtension.Add(".snd", "audio/basic");
			_mimeTypesByFileExtension.Add(".sol", "application/solids");
			_mimeTypesByFileExtension.Add(".spc", "text/x-speech");
			_mimeTypesByFileExtension.Add(".spl", "application/futuresplash");
			_mimeTypesByFileExtension.Add(".spr", "application/x-sprite");
			_mimeTypesByFileExtension.Add(".sprite", "application/x-sprite");
			_mimeTypesByFileExtension.Add(".src", "application/x-wais-source");
			_mimeTypesByFileExtension.Add(".ssi", "text/x-server-parsed-html");
			_mimeTypesByFileExtension.Add(".ssm", "application/streamingmedia");
			_mimeTypesByFileExtension.Add(".sst", "application/vnd.ms-pki.certstore");
			_mimeTypesByFileExtension.Add(".step", "application/step");
			_mimeTypesByFileExtension.Add(".stl", "application/sla");
			_mimeTypesByFileExtension.Add(".stp", "application/step");
			_mimeTypesByFileExtension.Add(".sv4cpio", "application/x-sv4cpio");
			_mimeTypesByFileExtension.Add(".sv4crc", "application/x-sv4crc");
			_mimeTypesByFileExtension.Add(".svf", "image/x-dwg");
			_mimeTypesByFileExtension.Add(".svr", "x-world/x-svr");
			_mimeTypesByFileExtension.Add(".swf", "application/x-shockwave-flash");
			_mimeTypesByFileExtension.Add(".t", "application/x-troff");
			_mimeTypesByFileExtension.Add(".talk", "text/x-speech");
			_mimeTypesByFileExtension.Add(".tar", "application/x-tar");
			_mimeTypesByFileExtension.Add(".tbk", "application/x-tbook");
			_mimeTypesByFileExtension.Add(".tcl", "application/x-tcl");
			_mimeTypesByFileExtension.Add(".tcsh", "text/x-script.tcsh");
			_mimeTypesByFileExtension.Add(".tex", "application/x-tex");
			_mimeTypesByFileExtension.Add(".texi", "application/x-texinfo");
			_mimeTypesByFileExtension.Add(".texinfo", "application/x-texinfo");
			_mimeTypesByFileExtension.Add(".text", "text/plain");
			_mimeTypesByFileExtension.Add(".tgz", "application/x-compressed");
			_mimeTypesByFileExtension.Add(".tif", "image/x-tiff");
			_mimeTypesByFileExtension.Add(".tiff", "image/x-tiff");
			_mimeTypesByFileExtension.Add(".tr", "application/x-troff");
			_mimeTypesByFileExtension.Add(".tsi", "audio/tsp-audio");
			_mimeTypesByFileExtension.Add(".tsp", "audio/tsplayer");
			_mimeTypesByFileExtension.Add(".tsv", "text/tab-separated-values");
			_mimeTypesByFileExtension.Add(".turbot", "image/florian");
			_mimeTypesByFileExtension.Add(".txt", "text/plain");
			_mimeTypesByFileExtension.Add(".uil", "text/x-uil");
			_mimeTypesByFileExtension.Add(".uni", "text/uri-list");
			_mimeTypesByFileExtension.Add(".unis", "text/uri-list");
			_mimeTypesByFileExtension.Add(".unv", "application/i-deas");
			_mimeTypesByFileExtension.Add(".uri", "text/uri-list");
			_mimeTypesByFileExtension.Add(".uris", "text/uri-list");
			_mimeTypesByFileExtension.Add(".ustar", "application/x-ustar");
			_mimeTypesByFileExtension.Add(".uu", "text/x-uuencode");
			_mimeTypesByFileExtension.Add(".uue", "text/x-uuencode");
			_mimeTypesByFileExtension.Add(".vcd", "application/x-cdlink");
			_mimeTypesByFileExtension.Add(".vcs", "text/x-vcalendar");
			_mimeTypesByFileExtension.Add(".vda", "application/vda");
			_mimeTypesByFileExtension.Add(".vdo", "video/vdo");
			_mimeTypesByFileExtension.Add(".vew", "application/groupwise");
			_mimeTypesByFileExtension.Add(".viv", "video/vivo");
			_mimeTypesByFileExtension.Add(".vivo", "video/vivo");
			_mimeTypesByFileExtension.Add(".vmd", "application/vocaltec-media-desc");
			_mimeTypesByFileExtension.Add(".vmf", "application/vocaltec-media-file");
			_mimeTypesByFileExtension.Add(".voc", "audio/x-voc");
			_mimeTypesByFileExtension.Add(".vos", "video/vosaic");
			_mimeTypesByFileExtension.Add(".vox", "audio/voxware");
			_mimeTypesByFileExtension.Add(".vqe", "audio/x-twinvq-plugin");
			_mimeTypesByFileExtension.Add(".vqf", "audio/x-twinvq");
			_mimeTypesByFileExtension.Add(".vql", "audio/x-twinvq-plugin");
			_mimeTypesByFileExtension.Add(".vrml", "application/x-vrml");
			_mimeTypesByFileExtension.Add(".vrt", "x-world/x-vrt");
			_mimeTypesByFileExtension.Add(".vsd", "application/x-visio");
			_mimeTypesByFileExtension.Add(".vst", "application/x-visio");
			_mimeTypesByFileExtension.Add(".vsw", "application/x-visio");
			_mimeTypesByFileExtension.Add(".w60", "application/wordperfect6.0");
			_mimeTypesByFileExtension.Add(".w61", "application/wordperfect6.1");
			_mimeTypesByFileExtension.Add(".w6w", "application/msword");
			_mimeTypesByFileExtension.Add(".wav", "audio/x-wav");
			_mimeTypesByFileExtension.Add(".wb1", "application/x-qpro");
			_mimeTypesByFileExtension.Add(".wbmp", "image/vnd.wap.wbmp");
			_mimeTypesByFileExtension.Add(".web", "application/vnd.xara");
			_mimeTypesByFileExtension.Add(".wiz", "application/msword");
			_mimeTypesByFileExtension.Add(".wk1", "application/x-123");
			_mimeTypesByFileExtension.Add(".wmf", "windows/metafile");
			_mimeTypesByFileExtension.Add(".wml", "text/vnd.wap.wml");
			_mimeTypesByFileExtension.Add(".wmlc", "application/vnd.wap.wmlc");
			_mimeTypesByFileExtension.Add(".wmls", "text/vnd.wap.wmlscript");
			_mimeTypesByFileExtension.Add(".wmlsc", "application/vnd.wap.wmlscriptc");
			_mimeTypesByFileExtension.Add(".word", "application/msword");
			_mimeTypesByFileExtension.Add(".wp", "application/wordperfect");
			_mimeTypesByFileExtension.Add(".wp5", "application/wordperfect");
			_mimeTypesByFileExtension.Add(".wp6", "application/wordperfect");
			_mimeTypesByFileExtension.Add(".wpd", "application/wordperfect");
			_mimeTypesByFileExtension.Add(".wq1", "application/x-lotus");
			_mimeTypesByFileExtension.Add(".wri", "application/x-wri");
			_mimeTypesByFileExtension.Add(".wrl", "application/x-world");
			_mimeTypesByFileExtension.Add(".wrz", "model/vrml");
			_mimeTypesByFileExtension.Add(".wsc", "text/scriplet");
			_mimeTypesByFileExtension.Add(".wsrc", "application/x-wais-source");
			_mimeTypesByFileExtension.Add(".wtk", "application/x-wintalk");
			_mimeTypesByFileExtension.Add(".xbm", "image/x-xbm");
			_mimeTypesByFileExtension.Add(".xdr", "video/x-amt-demorun");
			_mimeTypesByFileExtension.Add(".xgz", "xgl/drawing");
			_mimeTypesByFileExtension.Add(".xif", "image/vnd.xiff");
			_mimeTypesByFileExtension.Add(".xl", "application/excel");
			_mimeTypesByFileExtension.Add(".xla", "application/excel");
			_mimeTypesByFileExtension.Add(".xlb", "application/excel");
			_mimeTypesByFileExtension.Add(".xlc", "application/excel");
			_mimeTypesByFileExtension.Add(".xld", "application/excel");
			_mimeTypesByFileExtension.Add(".xlk", "application/excel");
			_mimeTypesByFileExtension.Add(".xll", "application/excel");
			_mimeTypesByFileExtension.Add(".xlm", "application/excel");
			_mimeTypesByFileExtension.Add(".xls", "application/excel");
			_mimeTypesByFileExtension.Add(".xlt", "application/excel");
			_mimeTypesByFileExtension.Add(".xlv", "application/excel");
			_mimeTypesByFileExtension.Add(".xlw", "application/excel");
			_mimeTypesByFileExtension.Add(".xm", "audio/xm");
			_mimeTypesByFileExtension.Add(".xml", "text/xml");
			_mimeTypesByFileExtension.Add(".xmz", "xgl/movie");
			_mimeTypesByFileExtension.Add(".xpix", "application/x-vnd.ls-xpix");
			_mimeTypesByFileExtension.Add(".xpm", "image/xpm");
			_mimeTypesByFileExtension.Add(".x-png", "image/png");
			_mimeTypesByFileExtension.Add(".xsr", "video/x-amt-showrun");
			_mimeTypesByFileExtension.Add(".xwd", "image/x-xwd");
			_mimeTypesByFileExtension.Add(".xyz", "chemical/x-pdb");
			_mimeTypesByFileExtension.Add(".z", "application/x-compress");
			_mimeTypesByFileExtension.Add(".zip", "application/zip");
			_mimeTypesByFileExtension.Add(".zoo", "application/octet-stream");
			_mimeTypesByFileExtension.Add(".zsh", "text/x-script.zsh");

			#endregion Build Mime Type List
		}

		#endregion Constructors

		#region Methods

		#region Static

		public static string GetMimeType(string extension)
		{
			string key = extension.StartsWith(".") ? extension : string.Format(".{0}", extension);
			if (MimeTypesByFileExtension.ContainsKey(key))
				return MimeTypesByFileExtension[key];

			// Default execution path
			return DefaultMimeType;
		}

		public static string BuildQueryString(NameValueCollection queryString)
		{
			var sob = new StringBuilder();
			sob.Append("?");
			foreach (string key in queryString)
			{
				sob
					.Append(HttpUtility.UrlEncode(key)).Append("=")
					.Append(HttpUtility.UrlEncode(queryString[key])).Append("&");
			}
			//if no values were added, remove '?' and return
			//	else remove last '&' and return
			return sob.ToString(0, sob.Length - 1);
		}

		public static string WebBreaks(string text)
		{
			if (String.IsNullOrEmpty(text))
				return text;
			return text.Replace("\r\n", "<br/>").Replace("\n", "<br/>"); //textarea's only add \n
		}

		public static string SavePostedFile(HttpPostedFile file, string baseUrl)
		{
			if (string.IsNullOrEmpty(baseUrl))
				throw new ArgumentNullException(baseUrl);

			string outputPath = HttpContext.Current.Server.MapPath(baseUrl);
			string outputFilename = string.Format("{0}{1}", StringUtility.GetRandomString(7), Path.GetExtension(file.FileName));

			file.SaveAs(string.Format("{0}{1}{2}", outputPath, (outputPath.EndsWith("\\") ? "" : "\\"), outputFilename));

			return string.Format("{0}{1}{2}", baseUrl, (baseUrl.EndsWith("/") ? "" : "/"), outputFilename);
		}

		public static bool UploadFileOverFTP(string ftpPath, string username, string password, string pathToFile)
		{
			// Validate parameters
			if (string.IsNullOrEmpty(ftpPath))
				throw new ArgumentNullException("ftpPath");
			if (string.IsNullOrEmpty(username))
				throw new ArgumentNullException("username");
			if (string.IsNullOrEmpty(password))
				throw new ArgumentNullException("password");
			if (string.IsNullOrEmpty(pathToFile))
				throw new ArgumentNullException("pathToFile");

			// Ensure the file exists
			if (!File.Exists(pathToFile))
				throw new FileNotFoundException(string.Format("Cannot find file: {0}", pathToFile));

			bool result = false;

			// Format the path on the server
			string path = ftpPath.EndsWith("/")
			              	? string.Format("{0}{1}", ftpPath, Path.GetFileName(pathToFile))
			              	: string.Format("{0}/{1}", ftpPath, Path.GetFileName(pathToFile));

			// Initialize an FTP request
			var request = WebRequest.Create(path) as FtpWebRequest;
			if (request != null)
			{
				request.Method = WebRequestMethods.Ftp.UploadFile;
				request.Credentials = new NetworkCredential(username, password);
				request.UsePassive = true;
				request.UseBinary = true;
				request.KeepAlive = false;
			}

			// Write the data to the request stream
			using (Stream requestStream = request.GetRequestStream())
			{
				using (Stream fileInput = File.OpenRead(pathToFile))
				{
					IOUtility.CopyStreamContents(fileInput, requestStream, new byte[32768]);
				}
				requestStream.Close();
			}

			// Get the response
			var response = request.GetResponse() as FtpWebResponse;
			if (response != null)
			{
				result = response.StatusCode == FtpStatusCode.ClosingData
				         || response.StatusCode == FtpStatusCode.CommandOK
				         || response.StatusCode == FtpStatusCode.FileActionOK;
			}

			return result;
		}

		#endregion Static

		#endregion Methods
	}
}