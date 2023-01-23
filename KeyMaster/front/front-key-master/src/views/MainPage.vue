<template>
  <Navbar></Navbar>
  <div class="test-title">
    <h1>Witaj</h1>
    <h1>tutaj bedziesz ćwiczyć na którtkich tesktach</h1>
  </div>
  <div class="lesson-container">
    <div class="quote">
      {{ text }}
      <div class="autor">
        {{ source }}
        {{ length }}
      </div>
    </div>
    <!--    {{ StatisticKeyboardClicks }}-->
    <div class="inputlesson">
      <input
        type="text"
        class="wordInput"
        v-model="inputText"
        :maxlength="length"
        :disabled="isEnabled"
        @keyup="checkForMistakes()"
      />
    </div>
    <Chart :lenght="length" errorPosition="errorPosition"></Chart>
    <h2>{{ error }}</h2>
    <h2>{{ statisticErrors }}</h2>
  </div>
  <Footer></Footer>
</template>

<script lang="ts">
import { Options, Vue } from "vue-class-component";
import Navbar from "@/components/Navbar.vue";
import Footer from "@/components/Footer.vue";
import Chart from "chart.js/auto";
import { defineComponent } from "vue";

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
      StatisticKeyboardClicks: -1,
      StatisticClicksPerMinute: 0,
      Mark: 0,
      isEnabled: false,
      clicks: 0,
      startTime: Date.now(),
      errorPosition: { x: 0, y: 0 },
    };
  },
  created() {
    this.fetchquote();
    this.checkForMistakes();
  },
  watch: {
    // call again the method if the route changes
    $route: "fetchData",
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
    addtoscores() {
      let token = "Bearer " + localStorage.getItem("token");
      this.axios.post(
        "https://localhost:5001/api/ScoresModels",
        {
          Wpm: 23,
          Errors: 2,
          Consistency: 32,
          Mark: 3,
          TextTitle: this.source,
        },
        {
          headers: {
            Authorization: token,
            "content-type": "text/json",
          },
        }
      );
    },
    checkForMistakes() {
      this.countClicksPerMinute();
      var textArray = this.text.split("");
      var inputTextArray = this.inputText.split("");
      //console.log(this.text);
      var errottmp = 0;
      for (let i = 0; i < inputTextArray.length; i++) {
        if (inputTextArray[i] != textArray[i]) {
          errottmp++;
          this.statisticErrors++;
          console.log("błąd");
          this.errorPosition.x = i;
          this.errorPosition.y = this.statisticErrors;
          //console.log(this.errorPosition);
        } else {
          console.log("git ");
        }
      }
      this.StatisticKeyboardClicks++;
      if (inputTextArray.length != 0 && inputTextArray.length == this.length) {
        this.addtoscores();
        this.isEnabled = true;
      }
      this.error = errottmp;
    },
    countClicksPerMinute() {
      this.clicks++;
      let currentTime = Date.now();
      let elapsedTime = (currentTime - this.startTime) / 1000; // convert to seconds
      let clicksPerMinute = this.clicks / (elapsedTime / 60);
      this.StatisticClicksPerMinute = clicksPerMinute;
    },
  },

  // mounted() {
  //   const ctx = document.getElementById("myChart");
  //
  //   const labels = [
  //     "0",
  //     "50",
  //     "100",
  //     "150",
  //     "200",
  //     "250",
  //     "300",
  //     "350",
  //     "400",
  //     "450",
  //     "500",
  //   ];
  //   const data = {
  //     labels: labels,
  //     datasets: [
  //       {
  //         label: "Popełnione błędy",
  //         data: [
  //           { x: 10, y: 2 },
  //           { x: 50, y: 0 },
  //           { x: 100, y: 10 },
  //           { x: 240, y: 10 },
  //         ],
  //         fill: true,
  //         borderColor: "rgb(192,75,75)",
  //         tension: 1,
  //       },
  //     ],
  //   };
  //   const myChart = new Chart(ctx, { type: "line", data: data });
  //   myChart;
  // },
});
</script>
