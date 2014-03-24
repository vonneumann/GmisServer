Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Data.SqlClient
Imports System.Configuration
Imports BusinessRules


<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class Service
     Inherits System.Web.Services.WebService


    '获取Webconfig配置文件中的数据库连接设置
    Private strConn As String = ConfigurationSettings.AppSettings("DBConnection")

    '通用查询
    <WebMethod()> Public Function GetCommonQueryInfo(ByVal strSql As String) As System.Data.DataSet
        Dim conn As New SqlConnection(strConn)
        conn.Open()
        Dim ts As SqlTransaction = conn.BeginTransaction
        Try
            Dim CommonQuery As New BusinessRules.CommonQuery(conn, ts)
            GetCommonQueryInfo = CommonQuery.GetCommonQueryInfo(strSql)
            ts.Commit()
        Catch dbEx As System.Data.DBConcurrencyException
            ts.Rollback()
            Throw dbEx
        Catch oEx As Exception
            ts.Rollback()
            Throw oEx
        Finally
            conn.Close()
            conn.Dispose()
        End Try
    End Function

End Class
