public class JogoVerificar
{
    private bool[] respostas;
    private bool[] respostasDadas;
    private int numJogo;

    public JogoVerificar(bool[] respostas,bool[] respostasDadas,int numJogo){
        this.respostas = respostas;
        this.respostasDadas = respostasDadas;
        this.numJogo = numJogo;
    }
    public void setRespostas(bool[] respostas){
        this.respostas=respostas;
    }
    public bool[] getRespostas(){
        return respostas;
    }
    public void setRespostasDadas(bool[] respostasDadas){
        this.respostasDadas=respostasDadas;
    }
    public bool[] getRespostasDadas(){
        return respostasDadas;
    }
    public int getNumJogo(){
        return numJogo;
    }
    public void setNumJogo(int numJogo){
        this.numJogo = numJogo;
    }
}
