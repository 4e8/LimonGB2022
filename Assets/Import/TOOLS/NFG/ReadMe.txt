
USAGE:

-To create a Generator go to Tools/NFG/New Generator

-The NFG windw can be located in Tools/NFG/Generator Window

-You can also select the Generator GameObject and click "Open Editor" on the NFG component


SAVING/LOADING:

-Generators are MonoBehaviour scripts.

-To save a generator configuration simply create a prefab out of the Generator GameObject.

-To load a generator drag and drop a saved Generator GameObject into the scene.


RUNTIME:

-NFG has a Runtime API for generating in game (Mesh Preprocessing is NOT included in runtime functionality).

-GetComponent<NaturalFormationGenerator>().DoGenerationRuntime();

-For a basic example check out 'RuntimeExample.cs' in NFG/Demo/RuntimExample.cs



For tutorials and more information please refer to here 

www.rephilgamingstudios.com/nfg