namespace BLH.ApproveIQ.Infrastructure.Services.Email.Templates;

public static class WelcomeEmailTemplate
{
    private const string NOTIFICATION_OF_ACCESS_TEMPLATE = $"" +
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
                                        $"<table width='95%' border='0' align='center' cellpadding='0' cellspacing='0' style='margin-bottom: 25px; padding-bottom: 25px'>" +
                                            $"<tr style='text-align: center'>" +
                                                $"<td>" +
                                                    $"<img width=500 height=125 src='{EmailTemplateConstants.IMAGE_CONTENTID_REPLACE_TOKEN}'>" +
                                                $"</td>" +
                                            $"</tr>" +
                                            $"<tr style='text-align: center'>" +
                                                $"<td>" +
                                                    $"<h2 style='color:#1e1e2d; font-weight:500; margin:0;font-size:32px;font-family:'Rubik',sans-serif;'>" +
                                                        $"Welcome to the {EmailTemplateConstants.APP_NAME_REPLACE_TOKEN} App!" +
                                                    $"</h2>" +
                                                $"</td>" +
                                            $"</tr>" +
                                        $"</table>" +
                                        $"<p style='color:#455056; font-size:15px;line-height:24px; margin:0; margin-bottom: 25px;'>" +
                                            $"Email: {EmailTemplateConstants.EMAIL_REPLACE_TOKEN}" +
                                        $"</p>" +
                                        $"<p style='color:#455056; font-size:15px;line-height:24px; margin:0; margin-bottom: 25px;'>" +
                                            $"To set your password, follow the Forgot Password link on the home page!" +
                                        $"</p>" +
                                        $"<div>" +
                                            $"<!--[if mso]>" +
                                                $"<v:roundrect xmlns:v='urn:schemas-microsoft-com:vml' xmlns:w='urn:schemas-microsoft-com:office:word' href='{EmailTemplateConstants.LINK_REPLACE_TOKEN}' style='height:40px;v-text-anchor:middle;width:200px;' " +
                                                $"arcsize='100%' stroke='f' fillcolor='#6AC5D6'> <w:anchorlock/>" +
                                                    $"<center>" +
                                                        $"<![endif]-->" +
                                                            $"<a href='{EmailTemplateConstants.LINK_REPLACE_TOKEN}' style='background-color:transparent;border:8px solid transparent; border-radius:40px;color:#000;display:inline-block;font-family:sans-serif;" +
                                                            $"font-size:13px;font-weight:bold;line-height:40px; text-align:center;text-decoration:none;width:200px;-webkit-text-size-adjust:none;'>" +
                                                                $"Go to Site" +
                                                            $"</a>" +
                                                        $"<!--[if mso]>" +
                                                    $"</center>" +
                                                $"</v:roundrect>" +
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

    public static string GetTemplate(string email, string link, string appName)
    {
        var template = NOTIFICATION_OF_ACCESS_TEMPLATE;
        template = template.Replace(EmailTemplateConstants.EMAIL_REPLACE_TOKEN, email);
        template = template.Replace(EmailTemplateConstants.LINK_REPLACE_TOKEN, link);
        template = template.Replace(EmailTemplateConstants.APP_NAME_REPLACE_TOKEN, appName);
        return template;
    }
}
