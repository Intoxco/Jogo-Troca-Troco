using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class JogoEntregarEvents : MonoBehaviour
{
    //Para voltar no menu - tela confirmacao
    [SerializeField] private GameObject filtroConfirmacao;
    //Para as animações da registradora e cliente
    [SerializeField] private Animator clienteAnimator=null;
    [SerializeField] private GameObject cliente;
    [SerializeField] private Animator RegistradoraAnimator=null;
    [SerializeField] private GameObject registradora;
    //Para mudar o numero da fase após entregar o troco
    [SerializeField] private GameObject fase;
    [SerializeField] private Texture textureFase2;
    [SerializeField] private Texture textureFase3;
    [SerializeField] private int numFase=0;
    //Para alterar os valores do custo, entregue e selecionado no computador
    [SerializeField] private TextMeshProUGUI textCusto;
    [SerializeField] private TextMeshProUGUI textValorEntregue;
    [SerializeField] private TextMeshProUGUI textValorSelecionado;

//Para gerar os valores de custo,troco e valor atual selecionado

    [SerializeField] private int numNota2Resposta;
    [SerializeField] private int numNota5Resposta;
    [SerializeField] private int numNota10Resposta;
    [SerializeField] private int numNota20Resposta;
    [SerializeField] private int troco;
    [SerializeField] private int valorEntregue;

    [SerializeField] private int custo;
    [SerializeField]private int num; // decide qual nota será usada para ser paga(5,10,20,50 ou 100)
//Para adicionar ou remover notas do valor Selecionado na caixa registradora
    [SerializeField] private int valorSelecionado;
    [SerializeField] private int numNota2=0;
    [SerializeField] private int numNota5=0;
    [SerializeField] private int numNota10=0;
    [SerializeField] private int numNota20=0;

    [SerializeField] private GameObject buttonMaisNota2;
    [SerializeField] private GameObject buttonMenosNota2;
    [SerializeField] private GameObject buttonMaisNota5;
    [SerializeField] private GameObject buttonMenosNota5;
    [SerializeField] private GameObject buttonMaisNota10;
    [SerializeField] private GameObject buttonMenosNota10;
    [SerializeField] private GameObject buttonMaisNota20;
    [SerializeField] private GameObject buttonMenosNota20;
//Para mostrar o feedback depois de enviar o troco
    [SerializeField] private int numTentativa = 0;
    [SerializeField] private Texture fala1;
    [SerializeField] private Texture fala2;
    [SerializeField] private Texture fala3;
    [SerializeField] private Texture fala4;

    [SerializeField] private GameObject fala;
    [SerializeField] private GameObject textTrocoDica;
    [SerializeField] private GameObject Gabarito;
    [SerializeField] private TextMeshProUGUI textGabarito;
    //Para score
    [SerializeField]private int[][] trocosEnviados = { new int[2] ,new int[2],new int[2]}; 

    [SerializeField]private int[] trocosCorretos = { 0,0,0};
    [SerializeField] private GameObject filtroFim;
    [SerializeField] private GameObject[] estrelas;
    [SerializeField] private Texture[] textureEstrelas;
    //fades
    [SerializeField] private GameObject fadeOut;
    [SerializeField] private GameObject fadeIn;
    //Pra audio
    [SerializeField] private AudioSource audioFala;
    [SerializeField] private AudioClip[] falas;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(!StaticData.som){
            registradora.GetComponent<AudioSource>().mute = true;
            audioFala.mute = true;
            
        }
        iniciarFase();
        StartCoroutine(EventFecharFadeIn());
        audioFala.PlayDelayed(3);
        
    }
    IEnumerator EventFecharFadeIn(){
        yield return new WaitForSeconds(2);
        fadeIn.SetActive(false);
    }
    IEnumerator EventTrocarCliente(){
        fala.SetActive(true);
        yield return new WaitForSeconds(2);
        fala.SetActive(false);
        clienteAnimator.Play("Cliente_sair",0,0.0f);
        yield return new WaitForSeconds(2);
        cliente.SetActive(false);
        cliente.SetActive(true);
        fala.GetComponent<RawImage>().texture = fala1;
        fala.SetActive(true);
        yield return new WaitForSeconds(2);
        audioFala.PlayOneShot(falas[0],1);
    }
    IEnumerator EventTrocarFase(Button button){
        if(numFase != 3)
            iniciarFase();
        switch(numFase){
            case 1:
                fase.GetComponent<RawImage>().texture= textureFase2;
                break;
            case 2: 
                fase.GetComponent<RawImage>().texture = textureFase3;
                break;
            default:
                break;
        }
        button.interactable = false;
        yield return new WaitForSeconds(8);
        button.interactable = true;
    }
    IEnumerator EventSairRegistradora(){
        RegistradoraAnimator.Play("Registradora_sair",0,0.0f);
        yield return new WaitForSeconds(1);
        registradora.SetActive(false);
    }
    IEnumerator EventIniciarFase(){
        gerarTroco();
        trocosCorretos[numFase] = troco;
        gerarValorEntregue();
        gerarCusto();
        textCusto.text= "R$ " + custo.ToString() + ",00";
        textValorEntregue.text = "R$ " + valorEntregue.ToString() + ",00";
        yield return new WaitForSeconds(1);
    }
    IEnumerator EventVoltarMenu(){
        fadeOut.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Menu");
    }
    IEnumerator EventJogarNovamente(){
        fadeOut.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("JogoEntregar");
    }
    IEnumerator EventJogarOutraFase(){
        fadeOut.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("JogoVerificar");
    }
    IEnumerator EventCalcularResultadoFase(){
    yield return new WaitForSeconds(1);
    fala.SetActive(false);
    yield return new WaitForSeconds(2);
    int somatorio=0;
    cliente.SetActive(false);
    for(int i=0;i<3;i++){
        if(trocosEnviados[i][0] == trocosCorretos[i]){
            somatorio += 2;
            trocosEnviados[i][1] = -1;
            }
        else if(trocosEnviados[i][1] == trocosCorretos[i])
            somatorio += 1;
    }
    switch(somatorio){
        case 0:
            estrelas[0].GetComponent<RawImage>().texture = textureEstrelas[0];
            estrelas[1].GetComponent<RawImage>().texture = textureEstrelas[0];
            estrelas[2].GetComponent<RawImage>().texture = textureEstrelas[0];
            break;
        case 1: 
            estrelas[0].GetComponent<RawImage>().texture = textureEstrelas[1];
            estrelas[1].GetComponent<RawImage>().texture = textureEstrelas[0];
            estrelas[2].GetComponent<RawImage>().texture = textureEstrelas[0];
            break;
        case 2:
            estrelas[0].GetComponent<RawImage>().texture = textureEstrelas[2];
            estrelas[1].GetComponent<RawImage>().texture = textureEstrelas[0];
            estrelas[2].GetComponent<RawImage>().texture = textureEstrelas[0];
            break;
        case 3:
            estrelas[0].GetComponent<RawImage>().texture = textureEstrelas[2];
            estrelas[1].GetComponent<RawImage>().texture = textureEstrelas[1];
            estrelas[2].GetComponent<RawImage>().texture = textureEstrelas[0];
            break;
        case 4:
            estrelas[0].GetComponent<RawImage>().texture = textureEstrelas[2];
            estrelas[1].GetComponent<RawImage>().texture = textureEstrelas[2];
            estrelas[2].GetComponent<RawImage>().texture = textureEstrelas[0];
            break;
        case 5:
            estrelas[0].GetComponent<RawImage>().texture = textureEstrelas[2];
            estrelas[1].GetComponent<RawImage>().texture = textureEstrelas[2];
            estrelas[2].GetComponent<RawImage>().texture = textureEstrelas[1];
            break;
        case 6:
            estrelas[0].GetComponent<RawImage>().texture = textureEstrelas[2];
            estrelas[1].GetComponent<RawImage>().texture = textureEstrelas[2];
            estrelas[2].GetComponent<RawImage>().texture = textureEstrelas[2];
            break;             
        }
    filtroFim.SetActive(true);
    JogoEntregar jogo = new (trocosCorretos,trocosEnviados,StaticData.pontuacaoEntregar.Count);
    StaticData.pontuacaoEntregar.Add(jogo);
    }
    IEnumerator EventFadeBotao(Button botao){
        botao.interactable = false;
        yield return new WaitForSeconds(3);
        botao.interactable = true;
    }
    public void sairRegistradora(){
        StartCoroutine(EventSairRegistradora());
    }
    public void entraRegistradora(){
        registradora.SetActive(true);
    }
    public void darTroco(Button button){
        if(valorSelecionado == troco ){
            fala.GetComponent<RawImage>().texture = fala2;
            audioFala.PlayOneShot(falas[1],1);
            textTrocoDica.SetActive(false);
            if(numFase!=2)
                StartCoroutine(EventTrocarCliente());
            else
                clienteAnimator.Play("Cliente_sair",0,0.0f);

           if(numTentativa <2)
                trocosEnviados[numFase][numTentativa]= valorSelecionado;
            numFase++;
            StartCoroutine(EventTrocarFase(button));
            resetarValores();
        }
        else{
            if(numTentativa == 0){
                trocosEnviados[numFase][numTentativa]= valorSelecionado;
                textTrocoDica.GetComponent<TextMeshProUGUI>().text = "R$ " + troco + ",00";
                fala.GetComponent<RawImage>().texture = fala3;
                audioFala.PlayOneShot(falas[2],1);
                StartCoroutine(EventFadeBotao(button));
                textTrocoDica.SetActive(true);
                trocosEnviados[numFase][numTentativa] = valorSelecionado;
                numTentativa++;
            }
            else if(numTentativa == 1){
                trocosEnviados[numFase][numTentativa]= valorSelecionado;
                numTentativa++;
                textGabarito.text = "";
                if(numNota2Resposta !=0)
                    textGabarito.text += numNota2Resposta.ToString() + " notas de $2\r\n";
                if(numNota5Resposta !=0)
                    textGabarito.text += numNota5Resposta.ToString() + " notas de $5\r\n";
                if(numNota10Resposta !=0)
                    textGabarito.text += numNota10Resposta.ToString() + " notas de $10\r\n";
                if(numNota20Resposta !=0)
                    textGabarito.text += numNota20Resposta.ToString() + " notas de $20\r\n";
                Gabarito.SetActive(true);
            }
            else{
                fala.GetComponent<RawImage>().texture = fala4;
                audioFala.PlayOneShot(falas[3],1);
                textTrocoDica.SetActive(false);
                if(numFase!=2)
                    StartCoroutine(EventTrocarCliente());
                numFase++;
                StartCoroutine(EventTrocarFase(button));
                resetarValores();
            }
        }
        if(numFase == 3){
            calcularResultadoFase();
        }
    }
    public void calcularResultadoFase(){
        StartCoroutine(EventCalcularResultadoFase());

    }
    public void resetarValores(){
        numNota2 =0;
        numNota5=0;
        numNota10=0;
        numNota20=0;
        buttonMenosNota2.SetActive(false);
        buttonMenosNota5.SetActive(false);
        buttonMenosNota10.SetActive(false);
        buttonMenosNota20.SetActive(false);
        buttonMaisNota2.SetActive(true);
        buttonMaisNota5.SetActive(true);
        buttonMaisNota10.SetActive(true);
        buttonMaisNota20.SetActive(true);
        valorSelecionado =0;
        numTentativa=0;
        Gabarito.SetActive(false);
        alterarValorSelecionado(0);
    }
    public void iniciarFase(){
        StartCoroutine(EventIniciarFase());
    }
    public void gerarTroco(){
        //maximos: 4 notas de 2, e 2 de vinte(2 de cinco, 1 de dez, uma de vinte), sendo que a nota de vinte não convertida tem 25% de chance de ser gerada
        numNota10Resposta = 0;
        numNota20Resposta= 0;
        numNota2Resposta = Random.Range(0,5);
        numNota5Resposta = Random.Range(0,3);
        numNota10Resposta = Random.Range(0,2);
        if(Random.Range(0,4)> 2)
            numNota20Resposta = 1;
        if(numNota5Resposta ==2){
            numNota5Resposta = 0;
            numNota10Resposta++;
            if(numNota10Resposta ==2){
                numNota10Resposta =0;
                numNota20Resposta++;
            }
        }
        troco = numNota2Resposta * 2 + numNota5Resposta * 5 + numNota10Resposta * 10 + numNota20Resposta * 20;
        if(troco == 0){
            gerarTroco();
        }
    }
    
    public void gerarValorEntregue(){
    //50% de chance de pagar com 5, 40% de chance de pagar com 10,5% de chance de pagar com 20, 2.5%,chance de pagar com 50, 1.25% de chance de pagar com 100
        if(troco <5)
            num =Random.Range(1,81);
    //53% de chance de pagar com 10,26% de chance de pagar com 20,13% de chance de pagar com 50,6% de chance de pagar com 100
        else if(troco <10)
            num = Random.Range(41,81);
    //57% de chance de pagar com 20,28% de chance de pagar com 50, 14% de chance de pagar com 100
        else if(troco<20)
            num = Random.Range(74,81);
    // 66% de chance de pagar com 50, 33% de chance de pagar com 100
        else    
            num = Random.Range(78,81);
    //1 a 40 = 5,41 a 73 = 10,74 a 77 = 20, 78 ou 79 =  50, 80 = 100
    if(num== 80)
        valorEntregue = 100;
    else if(num== 79 || num ==78)
        valorEntregue = 50;
    else if(num>72)
        valorEntregue = 20;
    else if(num>40)
        valorEntregue = 10;
    else
        valorEntregue = 5;
    }
    public void gerarCusto(){
        custo = valorEntregue - troco;
    }
    public void adicionarNota2(){
        alterarValorSelecionado(2);
        numNota2++;
        if(numNota2 == 9){
            buttonMaisNota2.SetActive(false);
        }
        else if(numNota2 == 1){
            buttonMenosNota2.SetActive(true);
        }
    }
    public void adicionarNota5(){
        alterarValorSelecionado(5);
        numNota5++;
        if(numNota5 == 3){
            buttonMaisNota5.SetActive(false);
        }
        else if(numNota5 == 1){
            buttonMenosNota5.SetActive(true);
        }
        }
    public void adicionarNota10(){
        alterarValorSelecionado(10);
        numNota10++;
        if(numNota10 == 3){
            buttonMaisNota10.SetActive(false);
        }
        else if(numNota10 == 1){
            buttonMenosNota10.SetActive(true);
        }
    }
    public void adicionarNota20(){
        alterarValorSelecionado(20);
        numNota20++;
        if(numNota20 == 2){
            buttonMaisNota20.SetActive(false);
        }
        else if(numNota20 == 1){
            buttonMenosNota20.SetActive(true);
        }
    }
    public void tirarNota2(){
        alterarValorSelecionado(-2);
        numNota2--;
        if(numNota2 == 0){
            buttonMenosNota2.SetActive(false);
        }
        else if(numNota2 == 8){
            buttonMaisNota2.SetActive(true);
        }
    }
    public void tirarNota5(){
        alterarValorSelecionado(-5);
        numNota5--;
        if(numNota5 == 0){
            buttonMenosNota5.SetActive(false);
        }
        else if(numNota5 == 2){
            buttonMaisNota5.SetActive(true);
        }
    }
    public void tirarNota10(){
        alterarValorSelecionado(-10);
        numNota10--;
        if(numNota10 == 0){
            buttonMenosNota10.SetActive(false);
        }
        else if(numNota10 == 2){
            buttonMaisNota10.SetActive(true);
        }
    }
    public void tirarNota20(){
        alterarValorSelecionado(-20);
        numNota20--;
        if(numNota20 == 0){
            buttonMenosNota20.SetActive(false);
        }
        else if(numNota20 == 1){
            buttonMaisNota20.SetActive(true);
        }
    }
    public void alterarValorSelecionado(int valor){
        valorSelecionado+= valor;
        textValorSelecionado.text = "R$ " + valorSelecionado.ToString() + ",00";
    }
    public void removerDica(GameObject botaoDica){
        botaoDica.SetActive(false);
    }
    public void voltarMenu(){
        StartCoroutine(EventVoltarMenu());
    }
    public void jogarNovamente(){
        StartCoroutine(EventJogarNovamente());
    }
    public void jogarOutraFase(){
        StartCoroutine(EventJogarOutraFase());
    }
    public void abrirTelaConfirmacao(){
        filtroConfirmacao.SetActive(true);
    }
    public void fecharTelaConfirmacao(){
        filtroConfirmacao.SetActive(false);
    }
}