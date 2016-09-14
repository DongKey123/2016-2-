using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(CameraMoveMent))]//누구(클래스,등등)를 위한 에디터인지 알려주기 위해
public class CameraMoveMentEditor : Editor {

    //SerializedProperty Camera;
    //SerializedProperty Pivot;
    //SerializedProperty wakeupTime;
    //SerializedProperty wakeupAngle;
    //SerializedProperty backwakeupTime;
    //SerializedProperty backwakeupAngle;
    //SerializedProperty ShakeHeadTime;
    //SerializedProperty ShakeHeadAngle;
    SerializedProperty anim;

    void OnEnable() //클릭하는순간
    {
        //Camera = serializedObject.FindProperty("Camera");
        //Pivot = serializedObject.FindProperty("Pivot");
        //wakeupTime = serializedObject.FindProperty("wakeupTime");
        //wakeupAngle = serializedObject.FindProperty("wakeupAngle");
        //backwakeupTime = serializedObject.FindProperty("backwakeupTime");
        //backwakeupAngle = serializedObject.FindProperty("backwakeupAngle");
        //ShakeHeadTime = serializedObject.FindProperty("ShakeHeadTime");
        //ShakeHeadAngle = serializedObject.FindProperty("ShakeHeadAngle");
        anim = serializedObject.FindProperty("anim");
    }

    public override void OnInspectorGUI() //보여지는동안
    {
        ////base : 상위 객체
        //base.OnInspectorGUI();

        // Automatic Management(변경사항을 자동으로 관리)
        serializedObject.Update(); //동기화,리플레쉬
        //EditorGUILayout.PropertyField(Camera, new GUIContent("카메라"));
        //EditorGUILayout.PropertyField(Pivot, new GUIContent("축"));
        //EditorGUILayout.PropertyField(wakeupTime, new GUIContent("이동시간"));
        //EditorGUILayout.PropertyField(wakeupAngle, new GUIContent("각도"));
        EditorGUILayout.PropertyField(anim, new GUIContent("Animator"));
        serializedObject.ApplyModifiedProperties();


        //Menual management (커스텀)
        CameraMoveMent myComponent = (CameraMoveMent)target;


        if (GUILayout.Button("카메라이동 1") == true)
        {
            myComponent.CameraCircleMove(1);
        }

        if (GUILayout.Button("리셋") == true)
        {
            myComponent.Reset();
        }

        //serializedObject.Update(); //동기화,리플레쉬
        //EditorGUILayout.PropertyField(backwakeupTime, new GUIContent("돌아가는 시간"));
        //EditorGUILayout.PropertyField(backwakeupAngle, new GUIContent("돌아가는 각도"));
        //serializedObject.ApplyModifiedProperties();

        if (GUILayout.Button("카메라이동 2") == true)
        {
            myComponent.CameraCircleMove(2);
        }

        //serializedObject.Update(); //동기화,리플레쉬
        //EditorGUILayout.PropertyField(ShakeHeadTime, new GUIContent("도리도리 시간"));
        //EditorGUILayout.PropertyField(ShakeHeadAngle, new GUIContent("도리도리 각도"));
        //serializedObject.ApplyModifiedProperties();

        if (GUILayout.Button("(시작하고 누르셈)카메라 도리도리 이동") == true)
        {
            myComponent.CameraCircleMove(3);
        }

        //if ( GUILayout.Button("Show My Window") == true)
        //{
        //    //MyEditorWindow.ShowWindow();
        //}

    }

}
