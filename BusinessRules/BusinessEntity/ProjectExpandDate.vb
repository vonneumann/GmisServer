Option Explicit On 

Imports System
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient

Public Class ProjectExpandDate

    Public Const Table_Project_ProjectExpandDate As String = "project_expand_date"

    '定义全局数据库连接对象
    Private conn As SqlConnection

    '定义全局数据库连接适配器
    Private dsCommand_ProjectExpandDate As SqlDataAdapter

    '定义查询命令
    Private GetProjectExpandDateInfoCommand As SqlCommand

    '定义事务
    Private ts As SqlTransaction


    '构造函数
    Public Sub New(ByVal DbConnection As SqlConnection, ByRef trans As SqlTransaction)
        MyBase.New()
        conn = DbConnection


        '实例化适配器
        dsCommand_ProjectExpandDate = New SqlDataAdapter

        '打开数据库连接
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If

        '引用外部事务
        ts = trans

        '填充适配器
        GetProjectExpandDateInfo("null")
    End Sub

    '获取项目评价信息
    Public Function GetProjectExpandDateInfo(ByVal strSQL_Condition_ProjectExpandDate As String) As DataSet

        Dim tempDs As New DataSet

        If GetProjectExpandDateInfoCommand Is Nothing Then

            GetProjectExpandDateInfoCommand = New SqlCommand("GetProjectExpandDateInfo", conn)
            GetProjectExpandDateInfoCommand.CommandType = CommandType.StoredProcedure
            GetProjectExpandDateInfoCommand.Parameters.Add(New SqlParameter("@Condition", SqlDbType.NVarChar))

        End If

        With dsCommand_ProjectExpandDate
            .SelectCommand = GetProjectExpandDateInfoCommand
            .SelectCommand.Transaction = ts
            GetProjectExpandDateInfoCommand.Parameters("@Condition").Value = strSQL_Condition_ProjectExpandDate
            .Fill(tempDs, Table_Project_ProjectExpandDate)
        End With

        Return tempDs

    End Function


    '更新项目评价信息
    Public Function UpdateProjectExpandDate(ByVal ProjectExpandDateSet As DataSet)

        If ProjectExpandDateSet Is Nothing Then
            Exit Function
        End If

        '如果记录集未发生任何变化，则退出过程
        If ProjectExpandDateSet.HasChanges = False Then
            Exit Function
        End If

        Dim bd As SqlCommandBuilder = New SqlCommandBuilder(dsCommand_ProjectExpandDate)

        With dsCommand_ProjectExpandDate
            .InsertCommand = bd.GetInsertCommand
            .UpdateCommand = bd.GetUpdateCommand
            .DeleteCommand = bd.GetDeleteCommand

            .InsertCommand.Transaction = ts
            .UpdateCommand.Transaction = ts
            .DeleteCommand.Transaction = ts

            .Update(ProjectExpandDateSet, Table_Project_ProjectExpandDate)

            ProjectExpandDateSet.AcceptChanges()
        End With


    End Function


End Class

