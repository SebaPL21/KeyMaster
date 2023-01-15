<template>
  <div class="lesson-container">
    <div class="quote">
      {{ text }}
      <div class="autor">
        {{ source }}
      </div>
    </div>

    <div class="inputlesson">
      <input
        type="text"
        class="wordInput"
        v-model="inputText"
        :maxlength="length"
      />
      {{ errors }}
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from "vue";
import Profile from "../views/Profil.vue";
import "@/styles/style.scss";
import "@/styles/content.scss";
export default defineComponent({
  name: "MainContent",
  components: {
    Profile,
  },

  data() {
    return {
      loading: false,
      post: null,
      result: String,
      text: "",
      source: "",
      length: "",
      inputText: "",
      errors: 0,
    };
  },
  computed: {},
  created() {
    // fetch the data when the view is created and the data is
    // already being observed
    // this.fetchData();
    this.fetchquote();
  },
  watch: {
    // call again the method if the route changes
    //'$route': 'fetchData'
  },
  methods: {
    fetchquote() {
      this.axios
        .get("https://localhost:5001/api/quotes/fetch")
        .then((resposne) => {
          console.log(resposne.data);
          this.source = resposne.data.Source;
          this.text = resposne.data.Text;
          this.length = resposne.data.Length;
        });
    },
    // checkForMistakes() {
    //   var text = this.text;
    //   var inputText = this.inputText;
    //   let errorsTMP = 0;
    //   var textArray = text.split("");
    //   var inputTextArray = inputText.split("");
    //   console.log(inputTextArray);
    //
    //   for (let i = 0; i < inputTextArray.length; i++) {
    //     if (inputTextArray[i] !== textArray[i]) {
    //       errorsTMP++;
    //       console.log("błąd");
    //       console.log("errors: " + errorsTMP);
    //       this.errors = errorsTMP;
    //     } else {
    //       console.log("git ");
    //     }
    //   }
    //   //console.log(textArray)
    //   //console.log(inputText);
    // },
  },
});
</script>
