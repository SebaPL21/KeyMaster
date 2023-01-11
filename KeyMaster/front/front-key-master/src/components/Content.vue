<template>  
  <div class="lesson-container">
    <div class="quote">
        {{ text }}
        <div class="autor">
        {{ source }}
     </div>
     </div>
     
     <div class="inputlesson">
        <input type="text" class="wordInput" 
        v-model="inputText"
        :maxlength="length"
        :on-keypress="checkForMistakes()"/> 
        {{ errors }}
     </div>
    </div>
</template>

<script lang='ts'>
import { defineComponent, ref } from 'vue'
import Profile from "../views/Profil.vue"
import '@/styles/style.scss'
import '@/styles/content.scss'
import { is } from '@babel/types';

type Forecasts = {
        date: string
    }[];

    interface Data {
        loading: boolean,
        post: null | Forecasts
    }
export default defineComponent({
    name: "MainContent",
    components:{
        Profile,
    },
    
    data(){
            return {
                loading: false,
                post: null,
                result: String,
                text: "",
                source: '',
                length: '',
                inputText:'',
                errors: 0
            };
        },
        computed:{
            
        },
        created() {
            // fetch the data when the view is created and the data is
            // already being observed
           // this.fetchData();
            this.fetchLesson();
        },
        watch: {
            // call again the method if the route changes
            //'$route': 'fetchData'
        },
        methods: {
            fetchLesson(){
               this.axios.get('api/lesson/fetch')
                .then(resposne => {
                    this.result = resposne.data
                   console.log(this.result)
                   this.source =resposne.data.source;
                   this.text = resposne.data.text;
                   this.length = resposne.data.length;
                    return this.result ;
                })
            },
            checkForMistakes(){
                var text = this.text;
                var inputText = this.inputText;
                let errorsTMP = 0
                var textArray = text.split('');
                var inputTextArray = inputText.split('')
                console.log(inputTextArray)

                for (let i = 0; i < inputTextArray.length; i++) {
                    if (inputTextArray[i] !== textArray[i]) {
                        errorsTMP++ 
                        console.log("błąd") 
                        console.log("errors: "+errorsTMP) 
                        this.errors=errorsTMP
                    }
                    else{
                        console.log("git ")  
                    }
                }
                //console.log(textArray)
                //console.log(inputText);
            },
                   
        },
    });
</script>
