Option Explicit On 

Imports System
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports BusinessRules.OAWorkflowXYDB


Public Class ImplOAReviewConclusion
    Implements IFlowTools

    '����ȫ�����ݿ����Ӷ���
    Private conn As SqlConnection

    '��������
    Private ts As SqlTransaction

    Private project As Project

    Private OAWorkflowXYDB As OAWorkflowXYDB.WorkflowServiceForXYDB

    Public Sub New(ByVal DbConnection As SqlConnection, ByRef trans As SqlTransaction)
        MyBase.New()
        conn = DbConnection


        '�����ݿ�����
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If

        '�����ⲿ����
        ts = trans

        project = New Project(conn, ts)

        OAWorkflowXYDB = New OAWorkflowXYDB.WorkflowServiceForXYDB()

    End Sub

    Public Function UseFlowTools(ByVal workFlowID As String, ByVal projectID As String, ByVal taskID As String, ByVal finishedFlag As String, ByVal userID As String) Implements IFlowTools.UseFlowTools
        Dim corporationCode As String
        Dim strSql As String

        '��������������������OA��
        '���ֶ���Ϣ
        Dim wrti(3) As OAWorkflowXYDB.WorkflowRequestTableField

        wrti(0) = New OAWorkflowXYDB.WorkflowRequestTableField()
        wrti(0).fieldName = "shenqr"
        wrti(0).fieldValue = "Ҷ����"
        wrti(0).view = True
        wrti(0).edit = True



        wrti(1) = New OAWorkflowXYDB.WorkflowRequestTableField()
        wrti(1).fieldName = "shenqbm"
        wrti(1).fieldValue = "01"
        wrti(1).view = True
        wrti(1).edit = True

        wrti(2) = New OAWorkflowXYDB.WorkflowRequestTableField()
        wrti(2).fieldName = "shenqly"
        wrti(2).fieldValue = "�ҵ���������"
        wrti(2).view = True
        wrti(2).edit = True

        '���ֶ�ֻ��һ������
        Dim wrtri(1) As OAWorkflowXYDB.WorkflowRequestTableRecord
        wrtri(0) = New OAWorkflowXYDB.WorkflowRequestTableRecord
        wrtri(0).workflowRequestTableFields = wrti

        Dim wmi As New OAWorkflowXYDB.WorkflowMainTableInfo()
        wmi.requestRecords = wrtri


        Dim wbi As New OAWorkflowXYDB.WorkflowBaseInfo()
        wbi.workflowId = "67"
        Dim wri As New OAWorkflowXYDB.WorkflowRequestInfo()
        wri.creatorId = "56"
        'wri.requestLevel = "2"
        wri.requestName = "�ҵĵ�һ���ӿڲ���"
        wri.workflowMainTableInfo = wmi
        wri.workflowBaseInfo = wbi

        Dim result As String
        result = OAWorkflowXYDB.doCreateWorkflowRequest(wri, 55)

        'OA�д�������������ǩ����


    End Function
End Class
