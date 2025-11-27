using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(EnemyConfigCreator))]
public class EnemyConfigCreatorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EnemyConfigCreator creator = (EnemyConfigCreator)target;

        GUILayout.Space(20f);

        if (GUILayout.Button("Create ScriptableObject"))
        {
            if (!creator.Validate(out var message))
            {
                Debug.LogError($"Error creating new enemy: {message}");
                return;
            }

            string folder = "Assets/Scripts/Enemy/Configs/";
            string path = $"{folder}{creator.EnemyName}.asset";

            if (AssetDatabase.LoadAssetAtPath<EnemySO>(path) != null)
            {
                Debug.LogError($"Enemy with name {creator.EnemyName} already exists.");
                return;
            }

            EnemySO newEnemy = new EnemySO.Builder()
            .WithName(creator.EnemyName)
            .WithMaxHealth(creator.MaxHealth)
            .WithBaseDamage(creator.BaseDamage)
            .WithSpeed(creator.Speed)
            .WithAnimatorController(creator.AnimationController)
            .WithBehavior(creator.BehaviorFactory)
            .Build();

            AssetDatabase.CreateAsset(newEnemy, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();


            creator.EnemyName = string.Empty;
            creator.MaxHealth = 0;
            creator.BaseDamage = 0;
            creator.Speed = 0;
            creator.AnimationController = null;
            creator.BehaviorFactory = null;

            EditorUtility.SetDirty(creator);


            Debug.Log($"{newEnemy.Name} asset generated at: {path}");

        }

    }


}

