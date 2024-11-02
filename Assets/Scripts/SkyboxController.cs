using UnityEngine;
using static ColorsGenerator;

public class SkyboxController : MonoBehaviour {

    [SerializeField] private Material garfieldSkybox;
    [SerializeField] private Material blueLagoonSkybox;
    [SerializeField] private Material jungleSkybox;
    [SerializeField] private Material barbieSkybox;
    [SerializeField] private Material darthVaderSkybox;

    public void SetupSkybox(MazeColorScheme mazeColorScheme)
    {
        RenderSettings.skybox =
            mazeColorScheme switch
            {
                MazeColorScheme.GARFIELD => garfieldSkybox,
                MazeColorScheme.BLUE_LAGOON => blueLagoonSkybox,
                MazeColorScheme.JUNGLE => jungleSkybox,
                MazeColorScheme.BARBIE => barbieSkybox,
                MazeColorScheme.DARTH_VADER => darthVaderSkybox,
                _ => null
            };
    }
}
