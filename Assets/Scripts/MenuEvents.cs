using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuEvents : MonoBehaviour
{
    public GameObject Audio;
    public GameObject fadeScreenIn;
    public GameObject fadeScreenOut; 

    public GameObject telaCreditos;
    public GameObject telaSobre;
    public GameObject filtro;
    public GameObject telaConfiguracao;
    public Sprite spriteSomDesligado;
    public Sprite spriteSomLigado;
    public Sprite spriteMusicaLigada;
    public Sprite spriteMusicaDesligada;
    public GameObject buttonSom;
    public GameObject buttonMusica;

    void Start()
    {
         StartCoroutine(EventStarter());
         if(StaticData.musica== false)
            FindAnyObjectByType<AudioSource>().Stop();
        }
    IEnumerator EventStarter(){
        yield return new WaitForSeconds(2);
        fadeScreenIn.SetActive(false);
    }
    IEnumerator EventOne(){
        fadeScreenOut.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("MenuEntregar");
    }
    IEnumerator EventTwo(){
        fadeScreenOut.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("MenuVerificar");
    }
    IEnumerator EventThree(){
        filtro.SetActive(true);
        telaCreditos.SetActive(true);
        yield return new WaitForEndOfFrame();
    }
    IEnumerator EventFour(){
        filtro.SetActive(true);
        telaSobre.SetActive(true);
        yield return new WaitForEndOfFrame();
    }
    IEnumerator EventFive(){
        filtro.SetActive(false);
        telaCreditos.SetActive(false);
        yield return new WaitForEndOfFrame();
    }
    IEnumerator EventSix(){
        filtro.SetActive(false);
        telaSobre.SetActive(false);
        yield return new WaitForEndOfFrame();
    }
    public void MenuButton(int eventPos){
        switch(eventPos){
            case 1:
                StartCoroutine(EventOne());
                break;
            case 2:
                StartCoroutine(EventTwo());
                break;
            case 3:
                StartCoroutine(EventThree());
                break;
            case 4:
                StartCoroutine(EventFour());
                break;
            case 5:
                StartCoroutine(EventFive());
                break;
            case 6:
                StartCoroutine(EventSix());
                break;
        }
    }
    public void alterarMusica(){
        if(StaticData.musica)
            pararMusica();
        else
            ativarMusica();
    }
    public void pararMusica(){
        StaticData.musica = false;
        buttonMusica.GetComponent<Image>().sprite = spriteMusicaDesligada;
        FindFirstObjectByType<AudioSource>().Pause();
    }
    public void ativarMusica(){
        StaticData.musica = true;
        buttonMusica.GetComponent<Image>().sprite = spriteMusicaLigada;
        FindFirstObjectByType<AudioSource>().Play();
    }
    public void pararSom(){
        StaticData.som = false;
        buttonSom.GetComponent<Image>().sprite = spriteSomDesligado;
    }
    public void ativarSom(){
        StaticData.som=true;
        buttonSom.GetComponent<Image>().sprite = spriteSomLigado;
    }
    public void alterarSom(){
        if(StaticData.som)
            pararSom();
        else
            ativarSom();
    }
    public void fecharConfiguracao(){
        telaConfiguracao.SetActive(false);
        filtro.SetActive(false);
    }
    public void abrirConfiguracao(){
        telaConfiguracao.SetActive(true);
        filtro.SetActive(true);
        if(StaticData.som)
            buttonSom.GetComponent<Image>().sprite = spriteSomLigado;
        else
            buttonSom.GetComponent<Image>().sprite = spriteSomDesligado;
        if(StaticData.musica)
            buttonMusica.GetComponent<Image>().sprite = spriteMusicaLigada;
        else
            buttonMusica.GetComponent<Image>().sprite = spriteMusicaDesligada;
    }
    public void fecharAplicacao(){
        Application.Quit();
    }
}
