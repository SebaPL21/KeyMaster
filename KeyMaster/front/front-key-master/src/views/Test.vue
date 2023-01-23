<template>
  <NavbarVue></NavbarVue>
  <v-container>
    <div class="test-title">
      <h1>Witaj na stronie testowej.</h1>
      <p>
        Tutaj będziesz mógł/mogła sprawdzić swoje postępy w praktycznym teście.
      </p>
    </div>
    <div class="lesson-container">
      <div class="quote">
        {{ text }}
        <div class="autor">
          {{ source }}
          {{ length }}
        </div>
      </div>

      <div class="inputlesson">
        <input
          type="text"
          class="wordInput"
          v-model="inputText"
          :maxlength="length"
          @keyup="checkForMistakes()"
        />
        <!--        {{ error }}-->
        <!--        {{ statisticErrors }}-->
      </div>
    </div>
  </v-container>
  <FooterVue></FooterVue>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import NavbarVue from "../components/Navbar.vue";

import FooterVue from "../components/Footer.vue";

export default defineComponent({
  name: "HelloWorld",
  components: {
    NavbarVue,
    FooterVue,
  },
  data() {
    return {
      result: "",
      text: "",
      source: "",
      length: "",
      inputText: "",
      error: 0,
      statisticErrors: 0,
    };
  },
  created() {
    // fetch the data when the view is created and the data is
    // already being observed
    // this.fetchData();
    this.fetchquote();
    this.checkForMistakes();
  },
  watch: {
    // call again the method if the route changes
    //'$route': 'fetchData'
  },
  methods: {
    fetchquote() {
      this.axios
        .get("https://localhost:5001/api/lesson/test")
        .then((resposne) => {
          console.log(resposne.data);
          this.source = resposne.data.Source;
          this.text = resposne.data.Text;
          this.length = resposne.data.Length;
        });
    },
    checkForMistakes() {
      var textArray = this.text.split("");
      var inputTextArray = this.inputText.split("");
      //console.log(this.text);
      var errottmp = 0;

      for (let i = 0; i < inputTextArray.length; i++) {
        if (inputTextArray[i] !== textArray[i]) {
          errottmp++;
          this.statisticErrors++;
          console.log("błąd");
        } else {
          console.log("git ");
        }
      }
      this.error = errottmp;
      //console.log(textArray)
      //console.log(inputText);
    },
  },
});
</script>
