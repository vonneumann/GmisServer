Imports System
Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient

Public Class ProjectOrganization
    Private moProjectOrganizationCommand As SqlCommand
    Private moConnection As SqlConnection
    Private moTransaction As SqlTransaction

    Public Sub New(ByRef Connection As SqlConnection)
        moConnection = Connection
        moTransaction = Nothing
    End Sub

    Public Sub New(ByRef Connection As SqlConnection, ByRef Transaction As SqlTransaction)
        moConnection = Connection
        moTransaction = Transaction
    End Sub

    Public Function GetProjectOrganization(ByVal condition As String) As DataSet
        Dim dstResult As DataSet = New DataSet("ProjectOrganizationDST")
        Dim da As SqlDataAdapter = New SqlDataAdapter()

        If condition Is Nothing Then
            condition = "NULL"
        End If

        Try
            If moConnection.State = ConnectionState.Closed Then
                moConnection.Open()
            End If
        Catch ex As System.Exception
            Throw ex
        End Try

        da.SelectCommand = New SqlCommand("dbo.GetTProjectOrganizationInfo", moConnection)
        da.SelectCommand.Transaction = moTransaction
        da.SelectCommand.CommandType = CommandType.StoredProcedure
        da.SelectCommand.Parameters.Add("@Condition", SqlDbType.NVarChar, 1000, "Condition")
        da.SelectCommand.Parameters("@Condition").Value = condition

        moProjectOrganizationCommand = da.SelectCommand

        Try
            da.Fill(dstResult, "TProjectOrganization")
        Catch ex As System.Exception
            Throw ex
        End Try

        Return dstResult
    End Function

    Public Function UpdateProjectOrganization(ByVal dstCommit As DataSet) As Int32
        Dim da As SqlDataAdapter = New SqlDataAdapter()

        If dstCommit Is Nothing Then
            Return -1
        End If
        If Not dstCommit.HasChanges() Then
            Return 0
        End If

        Try
            If moConnection.State = ConnectionState.Closed Then
                moConnection.Open()
            End If
        Catch ex As System.Exception
            Throw ex
        End Try

        If moProjectOrganizationCommand Is Nothing Then
            da.SelectCommand = New SqlCommand("dbo.GetTProjectOrganizationInfo", moConnection)
            da.SelectCommand.Transaction = moTransaction
            da.SelectCommand.CommandType = CommandType.StoredProcedure
            da.SelectCommand.Parameters.Add("@Condition", SqlDbType.NVarChar, 1000, "Condition")
            da.SelectCommand.Parameters("@Condition").Value = "NULL"
        Else
            moProjectOrganizationCommand.Connection = moConnection
            da.SelectCommand = moProjectOrganizationCommand
        End If

        Dim cmb As SqlCommandBuilder = New SqlCommandBuilder(da)

        da.InsertCommand = cmb.GetInsertCommand()
        da.InsertCommand.Transaction = moTransaction
        da.UpdateCommand = cmb.GetUpdateCommand()
        da.UpdateCommand.Transaction = moTransaction
        da.DeleteCommand = cmb.GetDeleteCommand()
        da.DeleteCommand.Transaction = moTransaction

        Try
            Return da.Update(dstCommit, "TProjectOrganization")
        Catch ex As System.Exception
            Throw ex
        End Try
    End Function
End Class
