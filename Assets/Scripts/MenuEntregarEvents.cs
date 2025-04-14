using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;
public class MenuEntregarEvents : MonoBehaviour
{
    public GameObject fadeOut;
    public GameObject fadeIn;
    [SerializeField] private GameObject telaPontuacao;
    [SerializeField] private GameObject buttonAnterior;
    [SerializeField] private GameObject buttonProximo;
    [SerializeField] private TextMeshProUGUI textTrocoCorreto;
    [SerializeField] private TextMeshProUGUI[] textTrocoEntregue;
    [SerializeField] private TextMeshProUGUI textFase;
    [SerializeField] private TextMeshProUGUI textJogo;
    [SerializeField] private GameObject estrela;
    [SerializeField] private Texture[] textureEstrelas;
    [SerializeField] private int jogoAtual = 1;
    [SerializeField] private int faseAtual = 1;
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
        SceneManager.LoadScene("JogoEntregar");
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
        if(StaticData.pontuacaoEntregar.Count!=0){
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
        JogoEntregar jogo = (JogoEntregar) StaticData.pontuacaoEntregar[jogoAtual-1];
        int trocoCorreto = jogo.getTrocosCorretos()[faseAtual-1];
        textTrocoCorreto.text = trocoCorreto.ToString() + ",00";
        int[] trocosEntregues = jogo.getTrocosEntregues()[faseAtual-1];
        textTrocoEntregue[0].text = trocosEntregues[0].ToString() + ",00";
        if(trocosEntregues[1] == -1)
            textTrocoEntregue[1].text = "NDA";
        else
            textTrocoEntregue[1].text = trocosEntregues[1].ToString() + ",00";
        if(trocoCorreto == trocosEntregues[0])
            estrela.GetComponent<RawImage>().texture = textureEstrelas[2];
        else if(trocoCorreto == trocosEntregues[1])
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
        else if(faseAtual==2 && jogoAtual == StaticData.pontuacaoEntregar.Count){
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
        else if (faseAtual == 3 && jogoAtual == StaticData.pontuacaoEntregar.Count){
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
