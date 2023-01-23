<template>
  <Navbar></Navbar>
  <div class="test-title">
    <h1>Witaj na stronie lekcyjnej.</h1>
    <p>
      Tutaj będziesz mógł/mogła poprawić swoją techinike oraz ułożenie rąk na
      klawiaturze
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
      <!--      {{ error }}-->
      <!--      {{ statisticErrors }}-->
    </div>
  </div>
  <Footer></Footer>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import Navbar from "@/components/Navbar.vue";
import Footer from "@/components/Footer.vue";

export default defineComponent({
  components: {
    Navbar,
    Footer,
  },
  data() {
    return {
      result: "",
      text: "",
      source: "",
      length: 0,
      inputText: "",
      error: 0,
      statisticErrors: 0,
      lessonId: "",
    };
  },
  created() {
    // fetch the data when the view is created and the data is
    // already being observed
    // this.fetchData();
    //this.getlessonId();
    this.fetchquote();
  },
  watch: {
    $route: "fetchquote",
  },
  methods: {
    getlessonId() {
      var idLesson = this.$route.params.id;
      console.log(idLesson);
      this.lessonId = String(idLesson);
      return idLesson;
    },
    fetchquote() {
      console.log(this.$route.params.lessonId);
      this.axios
        .get("https://localhost:5001/api/lesson/id", {
          params: { id: this.$route.params.lessonId },
        })
        .then((resposne) => {
          console.log(resposne.data);
          this.source = resposne.data.Source;
          this.text = resposne.data.Text;
          this.length = resposne.data.Length;
        });
    },
  },
});
</script>
