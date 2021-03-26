using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PopulationManager : MonoBehaviour
{
    public GameObject personPrefab;
    public int populationsSize = 10;

    public int xOffset = 0;
    public int yOffset = 0;

    List<GameObject> population = new List<GameObject>();

    public static float elapsed = 0f;

    int trialTime = 10;
    int generation = 1;

    GUIStyle guiStyle = new GUIStyle();

    private void OnGUI()
    {
        guiStyle.fontSize = 50;
        guiStyle.normal.textColor = Color.white;
        GUI.Label(new Rect(10, 10, 100, 20), "Generation " + generation, guiStyle);
        GUI.Label(new Rect(10, 65, 100, 20), "Trial time " + (int)elapsed, guiStyle);
    }

    void Start()
    {
        for (int i = 0; i < populationsSize; i++)
        {
            Camera cam = Camera.main;
            Vector3 pos = cam.ScreenToWorldPoint(new Vector3(Random.Range(0f + xOffset, Screen.width - xOffset), Random.Range(0f + yOffset, Screen.height - yOffset), 10f));
            GameObject go = Instantiate(personPrefab, pos, Quaternion.identity);

            go.GetComponent<DNA>().r = Random.Range(0f, 1f);
            go.GetComponent<DNA>().g = Random.Range(0f, 1f);
            go.GetComponent<DNA>().b = Random.Range(0f, 1f);

            population.Add(go);
        }
    }


    void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed > trialTime)
        {
            BreedNewPopulation();
            elapsed = 0;
        }
    }

    GameObject Breed(GameObject parent1, GameObject parent2)
    {
        Camera cam = Camera.main;
        Vector3 pos = cam.ScreenToWorldPoint(new Vector3(Random.Range(0f + xOffset, Screen.width - xOffset), Random.Range(0f + yOffset, Screen.height - yOffset), 10f));
        GameObject offspring = Instantiate(personPrefab, pos, Quaternion.identity);

        DNA dna1 = parent1.GetComponent<DNA>();
        DNA dna2 = parent2.GetComponent<DNA>();

        offspring.GetComponent<DNA>().r = Random.Range(0f, 10f) < 5f ? dna1.r : dna2.r;
        offspring.GetComponent<DNA>().g = Random.Range(0f, 10f) < 5f ? dna1.g : dna2.g;
        offspring.GetComponent<DNA>().b = Random.Range(0f, 10f) < 5f ? dna1.b : dna2.b;

        return offspring;
    }
    void BreedNewPopulation()
    {
        List<GameObject> newPopulation = new List<GameObject>();
        List<GameObject> sortedList = population.OrderBy(o => o.GetComponent<DNA>().timeToDie).ToList();

        population.Clear();

        for (int i = (int)(sortedList.Count / 2f) - 1; i < sortedList.Count - 1; i++)
        {
            population.Add(Breed(sortedList[i], sortedList[i - 1]));
            population.Add(Breed(sortedList[i + 1], sortedList[i]));
        }

        for (int i = 0; i < sortedList.Count; i++)
        {
            Destroy(sortedList[i]);
        }
        generation++;
    }
}
