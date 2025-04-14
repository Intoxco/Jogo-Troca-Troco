using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class JogoVerificarEvents : MonoBehaviour
{
//Objetos em cena
    [SerializeField] private GameObject recibo;
    [SerializeField] private GameObject fundoNotas;
    [SerializeField] private GameObject fadeIn;
    [SerializeField] private GameObject fadeOut;
    [SerializeField] private GameObject filtroFim;
    [SerializeField] private GameObject fala;
    [SerializeField] private GameObject[] estrelas;
    [SerializeField] private Texture[] textureEstrelas;
    [SerializeField] private GameObject[] notas;
    [SerializeField] private GameObject fase;
    [SerializeField] private Texture[] textureFases;
    [SerializeField] private Texture[] textureFalas ;
    [SerializeField] private Texture[] textureNotas;
    [SerializeField] private TextMeshProUGUI textValorDevolvido;
    [SerializeField] private TextMeshProUGUI textValorEntregue;
    [SerializeField] private TextMeshProUGUI textValorTotal;
    [SerializeField] private GameObject buttonVerificarTroco;
    [SerializeField] private GameObject buttonIncorreto;
    [SerializeField] private GameObject buttonCorreto;
    [SerializeField] private GameObject dica;
    [SerializeField] private GameObject filtroConfirmacao;
    [SerializeField] private GameObject botaoIncorreto;
    [SerializeField] private GameObject botaoCorreto;
    [SerializeField] private AudioClip[] falas;
    [SerializeField] private AudioSource audioFala;
    [SerializeField] private int[] valorNotas = new int[4]; //vê o valor de cada nota(nota 1,nota2,nota3,nota4)
    [SerializeField] private bool[] respostas = new bool[3];
    [SerializeField] private int valorTotal;
    [SerializeField] private int valorEntregue;
    [SerializeField] private int troco;
    [SerializeField] private int valorDevolvido;
    [SerializeField] private int[] valorNotasResposta = new int[4];//vê qual é o valor correto das notas
    [SerializeField] private int num; //pra gerar numeros aleatorios
    [SerializeField] private int numTentativa = 0;
    [SerializeField] private int numFase = 0;
    [SerializeField] private bool[] respostasDadas = new bool[3];
    void Start()
    {
        if(!StaticData.som){
            fundoNotas.GetComponent<AudioSource>().mute= true;
            audioFala.mute = true;
            recibo.GetComponent<AudioSource>().mute= true;
        }
        iniciarFase();
        StartCoroutine(EventTirarFadeIn());
        audioFala.PlayDelayed(2);
    }
    IEnumerator EventTirarFadeIn(){
        yield return new WaitForSeconds(2);
        fadeIn.SetActive(false);
    }
    IEnumerator EventJogarNovamente(){
        fadeOut.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("JogoVerificar");
    }
    IEnumerator EventIrMenu(){
        fadeOut.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Menu");
    }
    IEnumerator EventJogarOutraFase(){
        fadeOut.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("JogoEntregar");
    }


    IEnumerator EventTrocarFase(){
        buttonCorreto.GetComponent<Button>().interactable = false;
        buttonIncorreto.GetComponent<Button>().interactable = false;
        yield return new WaitForSeconds(5);
        fadeOut.SetActive(true);
        yield return new WaitForSeconds(2);
        fadeOut.SetActive(false);
        numTentativa =  0;
        switch(numFase){
            case 1:
                fase.GetComponent<RawImage>().texture = textureFases[1];
                break;
            case 2:
                fase.GetComponent<RawImage>().texture = textureFases[2];
                break;
        }
        fundoNotas.SetActive(false);
        buttonCorreto.GetComponent<Button>().interactable = true;
        buttonIncorreto.GetComponent<Button>().interactable = true;
        if(!buttonIncorreto.GetComponent<Button>().interactable)
            buttonIncorreto.GetComponent<Button>().interactable = true;
        else if(!buttonCorreto.GetComponent<Button>().interactable)
            buttonCorreto.GetComponent<Button>().interactable = true;
        recibo.SetActive(false);
        buttonVerificarTroco.SetActive(true);
        textValorDevolvido.text = "-------------";
        fala.GetComponent<RawImage>().texture = textureFalas[0];
        iniciarFase();
        fadeIn.SetActive(true);
        yield return new WaitForSeconds(2);
        fadeIn.SetActive(false);
        audioFala.PlayOneShot(falas[0],1);
    }
    IEnumerator EventFinalizarPartida(){
    yield return new WaitForSeconds(5);
    int numEstrelas = 0;
        for(int i = 0;i<3;i++){
            if(respostasDadas[i] == respostas[i])
                numEstrelas++;
        }
        switch(numEstrelas){
            case 0:
                estrelas[0].GetComponent<RawImage>().texture = textureEstrelas[0];
                estrelas[1].GetComponent<RawImage>().texture = textureEstrelas[0];
                estrelas[2].GetComponent<RawImage>().texture = textureEstrelas[0];
                break;
            case 1:
                estrelas[0].GetComponent<RawImage>().texture = textureEstrelas[2];
                estrelas[1].GetComponent<RawImage>().texture = textureEstrelas[0];
                estrelas[2].GetComponent<RawImage>().texture = textureEstrelas[0];
                break;
            case 2:
                estrelas[0].GetComponent<RawImage>().texture = textureEstrelas[2];
                estrelas[1].GetComponent<RawImage>().texture = textureEstrelas[2];
                estrelas[2].GetComponent<RawImage>().texture = textureEstrelas[0];
                break;
            case 3:
                estrelas[0].GetComponent<RawImage>().texture = textureEstrelas[2];
                estrelas[1].GetComponent<RawImage>().texture = textureEstrelas[2];
                estrelas[2].GetComponent<RawImage>().texture = textureEstrelas[2];
                break;
        }
        fundoNotas.SetActive(false);
        recibo.SetActive(false);
        filtroFim.SetActive(true);
        JogoVerificar jogo = new (respostas,respostasDadas,StaticData.pontuacaoVerificar.Count);
        StaticData.pontuacaoVerificar.Add(jogo);
    }
    IEnumerator EventFadeBotaoCorreto(){
        botaoCorreto.GetComponent<Button>().interactable = false;
        yield return new WaitForSeconds(3);
        botaoCorreto.GetComponent<Button>().interactable = true;
    }
    IEnumerator EventFadeBotaoIncorreto(){
        botaoIncorreto.GetComponent<Button>().interactable = false;
        yield return new WaitForSeconds(3);
        botaoIncorreto.GetComponent<Button>().interactable = true;
    }
    public void verificarResultado(bool respostaDada){
        if(respostaDada == respostas[numFase]){
            textValorDevolvido.text = "R$ " +valorDevolvido+",00";
            if(respostas[numFase]){
                fala.GetComponent<RawImage>().texture = textureFalas[1];
                audioFala.PlayOneShot(falas[1],1);
            }
            else{
                fala.GetComponent<RawImage>().texture = textureFalas[3];
                audioFala.PlayOneShot(falas[3],1);
            }
            if(numTentativa!= 1)
                respostasDadas[numFase] = respostaDada;
            numFase++;
            if(numFase != 3)
                StartCoroutine(EventTrocarFase());

        }else if(numTentativa != 1){
            respostasDadas[numFase] = respostaDada;
            if(respostas[numFase]){
                fala.GetComponent<RawImage>().texture = textureFalas[2];
                audioFala.PlayOneShot(falas[2],1);
                buttonIncorreto.GetComponent<Button>().interactable = false;
            }
            else{
                fala.GetComponent<RawImage>().texture = textureFalas[4];
                audioFala.PlayOneShot(falas[4],1);
                buttonCorreto.GetComponent<Button>().interactable = false;
            }
            textValorDevolvido.text = "R$ " +valorDevolvido+",00";
            numTentativa++;
        }
        if(numFase == 3){
            buttonCorreto.GetComponent<Button>().interactable = false;
            buttonIncorreto.GetComponent<Button>().interactable = false;
            finalizarPartida();
            }
    }
    public void finalizarPartida(){
        StartCoroutine(EventFinalizarPartida());
    }
    public void iniciarFase(){
        gerarValorDevolvido();
        gerarTroco();
        if(troco > valorNotas[0] + valorNotas[1] + valorNotas[2] + valorNotas[3])
            gerarValorEntregue(troco);
        else
            gerarValorEntregue(valorNotas[0] + valorNotas[1] + valorNotas[2] + valorNotas[3]);
        gerarValorTotal();
        //Coloca a textura correta nas notas de acordo com as notas selecionadas
        for(int i =0;i<4;i++){
            switch(valorNotas[i]){
                case 2:
                    notas[i].GetComponent<RawImage>().texture = textureNotas[0];
                    break;
                case 5:
                    notas[i].GetComponent<RawImage>().texture = textureNotas[1];
                    break;
                case 10:
                    notas[i].GetComponent<RawImage>().texture = textureNotas[2];
                    break;
                case 20:
                    notas[i].GetComponent<RawImage>().texture = textureNotas[3];
                    break;
            }
        }
        textValorTotal.text = "R$ "+valorTotal+",00";
        textValorEntregue.text = "R$ "+ valorEntregue+",00";
    }
    public void gerarRecibo(){
        recibo.SetActive(true);
    }
    public void gerarFundoNotas(){
        fundoNotas.SetActive(true);
        buttonVerificarTroco.SetActive(false);
    }
    public void gerarValorDevolvido(){
        for(int i=0;i<4;i++){
            valorNotas[i] = gerarValorNota();
        }
    }

    public void gerarTroco(){
        num = Random.Range(0,2);
        switch(num){
            case 0:
                respostas[numFase] = false;
                num = Random.Range(1,5);
                troco = valorNotas[0] + valorNotas[1] + valorNotas[2] + valorNotas[3];
                while(troco == valorNotas[0] + valorNotas[1] + valorNotas[2] + valorNotas[3]){
                    switch(num){
                        case 1:
                            valorNotasResposta[0] = gerarValorNota();
                            for(int i =1;i<4;i++)
                                valorNotasResposta[i] = valorNotas[i];
                            break;
                        case 2:
                            valorNotasResposta[0] = gerarValorNota();
                            valorNotasResposta[1]= gerarValorNota();
                            valorNotasResposta[2] = valorNotas[2];
                            valorNotasResposta[3] = valorNotas[3];
                            break;
                        case 3:
                            for(int i=0;i<3;i++)
                                valorNotasResposta[i] = gerarValorNota();
                            valorNotasResposta[3] = valorNotas[3];
                            break;
                        case 4:
                            for(int i = 0;i<4;i++)
                                valorNotasResposta[i] = gerarValorNota();
                            break;
                    }
                    troco = valorNotasResposta[0] + valorNotasResposta[1] + valorNotasResposta[2] + valorNotasResposta[3];
                    valorDevolvido = valorNotasResposta[0] + valorNotasResposta[1] + valorNotasResposta[2] + valorNotasResposta[3];
                }
                break;
            case 1:
                respostas[numFase] = true;
                troco = valorNotas[0]+ valorNotas[1] + valorNotas[2] + valorNotas[3];
                valorDevolvido = valorNotas[0] + valorNotas[1] + valorNotas[2] + valorNotas[3];
                break;

        }
        
    }
    public void gerarValorEntregue(int valor){
    /*
    O valor é o maior valor entre o troco entregue o troco correto
    chances: 
    valor = 8:
    8 em 15 nota 10, 4 em 15 nota 20, 2 em 15 nota 50, 1 em 15 nota 100
    valor>20
    4 em 7 nota 20, 2 em 7 nota 50, 1 em 7 nota 100
    valor>50
    2 em 3 nota 50, 1 em 3 nota 100
    valor < 50
    1 em 1 nota 100
    */
        if(valor == 8)
            num = Random.Range(1,16);
        else if(valor < 20)
            num = Random.Range(9,16);
        else if(valor < 50)
            num = Random.Range(13,16);
        else if(valor < 100)
            num =15;
        else
            num=15;
        if(num < 9)
            valorEntregue = 10;
        else if(num <13)
            valorEntregue = 20;
        else if(num < 15)
            valorEntregue = 50;
        else
            valorEntregue = 100;
    }
    public void gerarValorTotal(){
        valorTotal = valorEntregue - troco;
    }
    //pra gerar valores aleatorios pra notas
    public int gerarValorNota(){
        switch(Random.Range(1,5)){
                    case 1:
                        return 2;
                    case 2:
                        return 5;
                    case 3:
                        return 10;
                    case 4:
                        return 20;
            }
        return 0;
    }
    public void jogarNovamente(){
        StartCoroutine(EventJogarNovamente());
    }
    public void abrirTelaConfirmacao(){
        filtroConfirmacao.SetActive(true);

    }
    public void fecharTelaConfirmacao(){
        filtroConfirmacao.SetActive(false);
    }
    public void irMenu(){
        StartCoroutine(EventIrMenu());
    }
    public void jogarOutraFase(){
        StartCoroutine(EventJogarOutraFase());
    }
    public void fecharDica(){
        dica.SetActive(false);
    }
    public void aplicarFade(bool botao){

        switch (botao){
            case true:
                if(buttonIncorreto.GetComponent<Button>().interactable)
                StartCoroutine(EventFadeBotaoIncorreto());
                break;
            case false:
                if(buttonCorreto.GetComponent<Button>().interactable)
                StartCoroutine(EventFadeBotaoCorreto());
                break;
        }
    }
}
