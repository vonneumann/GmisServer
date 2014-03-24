Imports System.Xml
Imports System.Web
Imports System.Web.SessionState
Imports System.Web.Services.Protocols


Namespace WebService_CGMIS


Public Class [Global]
    Inherits System.Web.HttpApplication

#Region " �����������ɵĴ��� "

    Public Sub New()
        MyBase.New()

        '�õ�������������������ġ�
        InitializeComponent()

        '�� InitializeComponent() ����֮������κγ�ʼ��

    End Sub

    '���������������
    Private components As System.ComponentModel.IContainer

    'ע�⣺���¹��������������������
    '����ʹ�����������޸Ĵ˹��̡�
    '��Ҫʹ�ô���༭���޸�����
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        components = New System.ComponentModel.Container()
    End Sub

#End Region

    Private Function GetLogin(ByRef userName As String, ByRef password As String, ByRef system As String) As Boolean
        Dim context As HttpContext = Me.Context
        Dim HttpStream As System.IO.Stream = context.Request.InputStream

        'Save the current position of stream.
        Dim posStream As Long = HttpStream.Position

        'If the request contains an HTTP_SOAPACTION header, look at this message.
        If context.Request.ServerVariables("HTTP_SOAPACTION") Is Nothing Then
            Return False
        End If

        'Load the body of the HTTP message into an XML document.
        Dim dom As System.Xml.XmlDocument = New System.Xml.XmlDocument()

        Try
            dom.Load(HttpStream)

            'Reset the stream position.
            HttpStream.Position = posStream

            Dim nodeList As System.Xml.XmlNodeList

            'Bind to the Authentication header.
            nodeList = dom.GetElementsByTagName("System")
            If nodeList.Count > 0 Then
                system = nodeList.Item(0).InnerText
            End If

            nodeList = dom.GetElementsByTagName("UserName")
            If nodeList.Count > 0 Then
                userName = nodeList.Item(0).InnerText
            End If

            nodeList = dom.GetElementsByTagName("Password")
            If nodeList.Count > 0 Then
                password = nodeList.Item(0).InnerText
            End If

            Return True
        Catch e As Exception
            'Reset the position of stream.
            HttpStream.Position = posStream

            'Throw a SOAP exception.
            Dim name As XmlQualifiedName = New XmlQualifiedName("Load")
            Dim se As SoapException = New SoapException("Unable to read SOAP request", name, e)
            Throw se
        End Try

        Return False
    End Function

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' ��Ӧ�ó�������ʱ����
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' �ڻỰ����ʱ����
    End Sub

    Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' ��ÿ������ʼʱ����
    End Sub

    Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)
        ''���Զ�ʹ�ý��������֤ʱ����()
        'Dim lock As SenseLock = New SenseLock()

        'Try
        '    If lock.Open(6891, -1926, -4768) <> 0 Then
        '        Me.Context.AddError(New System.Security.SecurityException())
        '    End If
        'Catch ex As Exception
        '    Me.Context.AddError(ex)
        'Finally
        '    If Not lock Is Nothing Then
        '        lock.Close()
        '        lock = Nothing
        '    End If
        'End Try

        'Dim connectionString As String = system.Configuration.ConfigurationSettings.AppSettings("DBConnection")

        'Try
        '    Dim permissionObject As SWSystem.Security.Permissions.Permission = New SWSystem.Security.Permissions.Permission(connectionString)

        '    Dim system As String
        '    Dim userName As String
        '    Dim password As String

        '    If Not GetLogin(userName, password, system) Then
        '        Throw New SoapHeaderException("û��ָ������� SoapHeader: Authentication��", SoapException.MustUnderstandFaultCode)
        '    End If

        '    If Not permissionObject.VerifyLogin(userName, password, system) Then
        '        Throw New SoapException("InvalidPermission", SoapException.ClientFaultCode, Context.Request.Url.AbsoluteUri)
        '    End If
        'Catch
        '    Throw
        'End Try
    End Sub

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' �ڷ�������ʱ����
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' �ڻỰ����ʱ����
    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' ��Ӧ�ó������ʱ����
    End Sub

End Class

End Namespace
