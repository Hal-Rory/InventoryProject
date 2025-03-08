using UnityEditor;

public class MaterialLocationAssetPostprocessor : AssetPostprocessor
{
    void OnPreprocessModel()
    {
        // //if forcing the remap, remove this
        // var importSettingsMissing = assetImporter.importSettingsMissing;
        // if (!importSettingsMissing)
        //     return; // Asset imported already, do not process.
        //
        // //get the model importer, change the settings and then remap the model's materials using the materials folder
        // var modelImporter = assetImporter as ModelImporter;
        // modelImporter.SearchAndRemapMaterials(ModelImporterMaterialName.BasedOnMaterialName, ModelImporterMaterialSearch.Local);
    }
}