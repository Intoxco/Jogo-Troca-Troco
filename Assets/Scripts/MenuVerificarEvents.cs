using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;
public class MenuVerificarEvents : MonoBehaviour
{
    public GameObject fadeOut;
    public GameObject fadeIn;
    public int jogoAtual= 1;
    public int faseAtual= 1;
    public GameObject telaPontuacao;
    [SerializeField] private TextMeshProUGUI textFase;
    [SerializeField] private TextMeshProUGUI textJogo;
    [SerializeField] private TextMeshProUGUI textRespostaEscolhida;
    [SerializeField] private TextMeshProUGUI textRespostaCorreta;
    [SerializeField] private GameObject buttonProximo;
    [SerializeField] private GameObject buttonAnterior;
    [SerializeField] private GameObject estrela;
    [SerializeField] private Texture[] textureEstrelas;
    [SerializeField] private GameObject telaNenhumRegistro;
    [SerializeField] private GameObject telaTutorial;
    [SerializeField] private VideoPlayer video;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(EventFecharFadeIn());
    }
    IEnumerator EventFecharFadeIn(){
        yield return new WaitForSeconds(2);
        fadeIn.SetActive(false);
    }
    IEnumerator EventIniciarJogo(){
        fadeOut.SetActive(true);
        yield return new WaitForSeconds(2);
        Destroy(GameObject.Find("Audio"));
        SceneManager.LoadScene("JogoVerificar");
    }
    IEnumerator EventVoltarMenu(){
        fadeOut.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Menu");
    }
    public void iniciarJogo(){
        StartCoroutine(EventIniciarJogo());
    }
    public void voltarMenu(){
        StartCoroutine(EventVoltarMenu());
    }
    public void abrirPontuacao(){
        if(StaticData.pontuacaoVerificar.Count!=0){
            telaPontuacao.SetActive(true);
            carregarPontuacao();
        }
        else{
            telaNenhumRegistro.SetActive(true);
        }
    }
    public void carregarPontuacao(){
        textJogo.text = jogoAtual.ToString();
        textFase.text = faseAtual.ToString();
        JogoVerificar jogo = (JogoVerificar) StaticData.pontuacaoVerificar[jogoAtual-1];
        bool respostaCorreta = jogo.getRespostas()[faseAtual-1];
        if(respostaCorreta)
            textRespostaCorreta.text = "CORRETO";
        else
            textRespostaCorreta.text = "INCORRETO";
        bool respostaDada = jogo.getRespostasDadas()[faseAtual-1];
        if(respostaDada)
            textRespostaEscolhida.text = "CORRETO";
        else
            textRespostaEscolhida.text = "INCORRETO";
        if(respostaDada == respostaCorreta)
            estrela.GetComponent<RawImage>().texture = textureEstrelas[1];
        else
            estrela.GetComponent<RawImage>().texture = textureEstrelas[0];    
    }
    public void avancarPontuacao(){
        if(faseAtual==3){
            jogoAtual++;
            faseAtual=1;
            carregarPontuacao();
        }
        else if(faseAtual==2 && jogoAtual == StaticData.pontuacaoVerificar.Count){
            faseAtual++;
            buttonProximo.SetActive(false);
            carregarPontuacao();
        }
        else if(faseAtual == 1 && jogoAtual == 1){
                faseAtual++;
                buttonAnterior.SetActive(true);
                carregarPontuacao();
            }
        else{
            faseAtual++;
            carregarPontuacao();
        }
    }
    public void voltarPontuacao(){
        if(faseAtual == 1){
            jogoAtual--;
            faseAtual = 3;
            carregarPontuacao();

        }
        else if(faseAtual == 2 && jogoAtual == 1){
            faseAtual--;
            buttonAnterior.SetActive(false);
            carregarPontuacao();
        }
        else if (faseAtual == 3 && jogoAtual == StaticData.pontuacaoVerificar.Count){
            faseAtual--;
            buttonProximo.SetActive(true);
            carregarPontuacao();
        }
        else{
            faseAtual--;
            carregarPontuacao();
        }
    }
    public void fecharPontuacao(){
        faseAtual = 1;
        jogoAtual = 1;
        telaPontuacao.SetActive(false);
        buttonAnterior.SetActive(false);
        buttonProximo.SetActive(true);
    }
    public void fecharTelaNenhumRegistro(){
        telaNenhumRegistro.SetActive(false);
    }
    public void abrirTelaTutorial(){
        telaTutorial.SetActive(true);
        video.Play();
        if(StaticData.musica)
            FindFirstObjectByType<AudioSource>().Stop();
    }
    public void fecharTelaTutorial(){
        telaTutorial.SetActive(false);
        video.time = 0;
        video.Stop();
        if(StaticData.musica)
            FindFirstObjectByType<AudioSource>().Play();
    }
}
