namespace BLH.ApproveIQ.Infrastructure.Services.Email.Templates;

public static class AssignedSessionEmailTemplate
{
    private const string ASSIGNED_SESSION_TEMPLATE = $"" +
        $"<!doctype html>" +
        $"<html lang='en-US'>" +
            $"<head>" +
                $"<meta content='text/html; charset=utf-8' http-equiv='Content-Type'/>" +
                $"<title>Reset Password Email Template</title>" +
                $"<meta name='description' content='Assigned Session Email Template.'>" +
            $"</head>" +
            $"<body marginheight='0' topmargin='0' marginwidth='0' style='margin: 0px; background-color: #f2f3f8;' leftmargin='0'>" +
                $"<table cellspacing='0' border='0' cellpadding='0' width='100%' bgcolor='#f2f3f8' style='@import url(https://fonts.googleapis.com/css?family=Rubik:300,400,500,700|Open+Sans:300,400,600,700); " +
                $"font-family: 'Open Sans', sans-serif;'>" +
                $"<tr>" +
                    $"<td>" +
                    $"<table style='background-color: #f2f3f8; max-width:670px; margin:0 auto;' width='100%' border='0' align='center' cellpadding='0' cellspacing='0'>" +
                        $"<tr>" +
                            $"<td style='height:80px;'>&nbsp;</td>" +
                        $"</tr>" +
                        $"<tr>" +
                            $"<td>" +
                                $"<table width='95%' border='0' align='center' cellpadding='0' cellspacing='0' style='max-width:670px;background:#fff; border-radius:3px; text-align:center;-webkit-box-shadow:0 6px 18px 0 rgba(0,0,0,.06); " +
                                $"-moz-box-shadow:0 6px 18px 0 rgba(0,0,0,.06);box-shadow:0 6px 18px 0 rgba(0,0,0,.06);'>" +
                                $"<tr>" +
                                    $"<td style='height:40px;'>&nbsp;</td>" +
                                $"</tr>" +
                                $"<tr>" +
                                    $"<td style='padding:0 35px;'>" +
                                        $"<table width='95%' border='0' align='center' cellpadding='0' cellspacing='0'>" +
                                            $"<tr style='text-align: center'>" +
                                                $"<td>" +
                                                    $"<img width=500 height=125 src='{EmailTemplateConstants.IMAGE_CONTENTID_REPLACE_TOKEN}'>" +
                                                $"</td>" +
                                            $"</tr>" +
                                        $"</table>" +


                                        $"<h1 style='color:#1e1e2d; font-weight:500; margin:0;font-size:32px;font-family:'Rubik',sans-serif;'>" +
                                            $"Tutor Id:  {EmailTemplateConstants.TUTOR_ID_REPLACE_TOKEN}" +
                                        $"</h1>" +
                                        $"<span style='display:inline-block; vertical-align:middle; margin:29px 0 26px; border-bottom:1px solid #cecece; width:100px;'></span>" +
                                        $"<p style='color:#455056; font-size:15px;line-height:24px; margin:0; margin-bottom: 25px;'>" +
                                            $"Session Id: {EmailTemplateConstants.SESSION_ID_REPLACE_TOKEN}" +
                                        $"</p>" +

                                        $"<div>" +
                                        $"</div>" +
                                    $"</td>" +
                                $"</tr>" +
                                $"<tr>" +
                                    $"<td style='height:40px;'>&nbsp;</td>" +
                                $"</tr>" +
                                $"</table>" +
                            $"</td>" +
                        $"<tr>" +
                            $"<td style='height:20px;'>&nbsp;</td>" +
                        $"</tr>" +
                        $"</table>" +
                    $"</td>" +
                $"</tr>" +
                $"</table>" +
            $"</body>" +
        $"</html>";



    public static string GetTemplate(List<Guid> SessionIds, Guid TutorId)
    {
        var template = ASSIGNED_SESSION_TEMPLATE;
        // template = template.Replace(EmailTemplateConstants.SESSION_ID_REPLACE_TOKEN, SessionId.ToString());
        template = template.Replace(EmailTemplateConstants.TUTOR_ID_REPLACE_TOKEN, TutorId.ToString());
        return template;
    }
}



// public static class ForgotPasswordEmailTemplate
// {
/*
    private const string FORGOT_PASSWORD_TEMPLATE = $"" +
        $"<!doctype html>" +
        $"<html lang='en-US'>" +
            $"<head>" +
                $"<meta content='text/html; charset=utf-8' http-equiv='Content-Type'/>" +
                $"<title>Reset Password Email Template</title>" +
                $"<meta name='description' content='Reset Password Email Template.'>" +
            $"</head>" +
            $"<body marginheight='0' topmargin='0' marginwidth='0' style='margin: 0px; background-color: #f2f3f8;' leftmargin='0'>" +
                $"<table cellspacing='0' border='0' cellpadding='0' width='100%' bgcolor='#f2f3f8' style='@import url(https://fonts.googleapis.com/css?family=Rubik:300,400,500,700|Open+Sans:300,400,600,700); " +
                $"font-family: 'Open Sans', sans-serif;'>" +
                $"<tr>" +
                    $"<td>" +
                    $"<table style='background-color: #f2f3f8; max-width:670px; margin:0 auto;' width='100%' border='0' align='center' cellpadding='0' cellspacing='0'>" +
                        $"<tr>" +
                            $"<td style='height:80px;'>&nbsp;</td>" +
                        $"</tr>" +
                        $"<tr>" +
                            $"<td>" +
                                $"<table width='95%' border='0' align='center' cellpadding='0' cellspacing='0' style='max-width:670px;background:#fff; border-radius:3px; text-align:center;-webkit-box-shadow:0 6px 18px 0 rgba(0,0,0,.06); " +
                                $"-moz-box-shadow:0 6px 18px 0 rgba(0,0,0,.06);box-shadow:0 6px 18px 0 rgba(0,0,0,.06);'>" +
                                $"<tr>" +
                                    $"<td style='height:40px;'>&nbsp;</td>" +
                                $"</tr>" +
                                $"<tr>" +
                                    $"<td style='padding:0 35px;'>" +
                                        $"<table width='95%' border='0' align='center' cellpadding='0' cellspacing='0'>" +
                                            $"<tr style='text-align: center'>" +
                                                $"<td>" +
                                                    $"<img width=500 height=125 src='{EmailTemplateConstants.IMAGE_CONTENTID_REPLACE_TOKEN}'>" +
                                                $"</td>" +
                                            $"</tr>" +
                                        $"</table>" +
                                        $"<h1 style='color:#1e1e2d; font-weight:500; margin:0;font-size:32px;font-family:'Rubik',sans-serif;'>" +
                                            $"You have requested to reset your password." +
                                        $"</h1>" +
                                        $"<span style='display:inline-block; vertical-align:middle; margin:29px 0 26px; border-bottom:1px solid #cecece; width:100px;'></span>" +
                                        $"<p style='color:#455056; font-size:15px;line-height:24px; margin:0; margin-bottom: 25px;'>" +
                                            $"A unique link to reset your password has been generated for you. To reset your password, click the following link and follow the instructions." +
                                        $"</p>" +
                                        $"<div>" +
                                            $"<!--[if mso]>" +
                                                // $"<v:roundrect xmlns:v='urn:schemas-microsoft-com:vml' xmlns:w='urn:schemas-microsoft-com:office:word' href='{EmailTemplateConstants.PASSWORD_LINK_REPLACE_TOKEN}' style='height:40px;v-text-anchor:middle;width:200px;' " +
                                                // $"arcsize='100%' stroke='f' fillcolor='#6AC5D6'> <w:anchorlock/>" +
                                                //     $"<center>" +
                                                //         $"<![endif]-->" +
                                                //             $"<a href='{EmailTemplateConstants.PASSWORD_LINK_REPLACE_TOKEN}' style='background-color:transparent;border:8px solid transparent; border-radius:40px;color:#000;display:inline-block;font-family:sans-serif;" +
                                                //             $"font-size:13px;font-weight:bold;line-height:40px; text-align:center;text-decoration:none;width:200px;-webkit-text-size-adjust:none;'>" +
                                                //                 $"Reset Password" +
                                                //             $"</a>" +
                                                //         $"<!--[if mso]>" +
                                                //     $"</center>" +
                                                // $"</v:roundrect>" +
                                            $"<![endif]-->" +
                                        $"</div>" +
                                    $"</td>" +
                                $"</tr>" +
                                $"<tr>" +
                                    $"<td style='height:40px;'>&nbsp;</td>" +
                                $"</tr>" +
                                $"</table>" +
                            $"</td>" +
                        $"<tr>" +
                            $"<td style='height:20px;'>&nbsp;</td>" +
                        $"</tr>" +
                        $"</table>" +
                    $"</td>" +
                $"</tr>" +
                $"</table>" +
            $"</body>" +
        $"</html>";
*/
//     public static string GetTemplate(string link)
//     {
//         var template = FORGOT_PASSWORD_TEMPLATE;
//         template = template.Replace(EmailTemplateConstants.PASSWORD_LINK_REPLACE_TOKEN, link);
//         return template;
//     }
// }
