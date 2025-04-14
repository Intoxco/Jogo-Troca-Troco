    public class JogoEntregar
    {
        private int[] trocosCorretos;
        private int[][] trocosEntregues;
        private int numJogo;

        public JogoEntregar(int [] trocosCorretos,int[][] trocosEntregues,int numJogo){
            this.trocosCorretos = trocosCorretos;
            this.trocosEntregues = trocosEntregues;
            this.numJogo = numJogo;
        }
        public void setTrocosCorretos(int[] trocosCorretos){
        this.trocosCorretos= trocosCorretos;
    }
    public int[] getTrocosCorretos(){
        return trocosCorretos;
    }
    public void setTrocosEntregues(int[][] trocosEntregues){
        this.trocosEntregues = trocosEntregues;
    }
    public int[][] getTrocosEntregues(){
        return trocosEntregues;
    }
    public int getNumJogo(){
        return numJogo;
    }
    public void setNumJogo(int numJogo){
        this.numJogo = numJogo;
    }
    }