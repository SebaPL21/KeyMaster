<template>
  <NavbarVue></NavbarVue>
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
    <div>
      <div class="chart">
        <canvas id="myChart"></canvas>
      </div>
      <div class="afterLessonStats" v-if="afterLesson">
        <h2>Popełnione błędy {{ error }}</h2>
        <h2>Dokładność {{ StatisticAccuracy }} %</h2>
        <h2>CPM {{ StatisticClicksPerMinute }}</h2>
      </div>
    </div>
  </div>
  <FooterVue></FooterVue>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import NavbarVue from "../components/Navbar.vue";

import FooterVue from "../components/Footer.vue";
import { Chart } from "chart.js/auto";

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
      length: 0,
      inputText: "",
      error: 0,
      statisticErrors: 0,
      StatisticKeyboardClicks: -1,
      StatisticClicksPerMinute: 0,
      StatisticAccuracy: 0,
      Mark: 0,
      isEnabled: false,
      clicks: 0,
      startTime: Date.now(),
      errorPosition: [{ x: 0, y: 0 }],
      afterLesson: false,
      timeoutId: 0,
      labels: [""],
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
    addtoscores() {
      let token = "Bearer " + localStorage.getItem("token");
      this.axios.post(
        "https://localhost:5001/api/ScoresModels",
        {
          Cpm: this.StatisticKeyboardClicks,
          Errors: this.error,
          Accuracy: this.StatisticAccuracy.toFixed(0),
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
      var errottmp = 0;
      var failAt = 0;
      let errorpos = [{ x: 0, y: 0 }];
      for (let i = 0; i < inputTextArray.length; i++) {
        if (inputTextArray[i] != textArray[i]) {
          errottmp++;
          this.statisticErrors++;
          console.log("błąd");
          failAt = i;
          errorpos.push({ x: failAt, y: errottmp });
        } else {
          console.log("git ");
        }
      }
      this.error = errottmp;

      this.errorPosition = errorpos;
      console.log(this.labels);
      this.StatisticKeyboardClicks++;
      if (inputTextArray.length != 0 && inputTextArray.length == this.length) {
        this.isEnabled = true;
        this.afterLesson = true;
        this.countAccutacy();
        this.errorPosition.forEach((i) => {
          this.labels.push(i.x.toString());
        });

        this.drawChart();
        this.addtoscores();
      }
    },
    countClicksPerMinute() {
      this.clicks++;
      let currentTime = Date.now();
      let elapsedTime = (currentTime - this.startTime) / 1000;
      let clicksPerMinute = this.clicks / (elapsedTime / 60);
      let cpm = Math.round(clicksPerMinute);
      this.StatisticClicksPerMinute = cpm;
    },
    countAccutacy() {
      console.log((this.length - this.error) / this.length);
      let acc = ((this.length - this.error) / this.length) * 100;
      let accuracy = Math.round(acc);
      this.StatisticAccuracy = accuracy;
    },
    drawChart() {
      const ctx = "myChart";
      const data = {
        labels: this.labels,
        datasets: [
          {
            label: "Wykres blędów",
            data: this.errorPosition,
            borderColor: "rgb(192,75,75)",
            tension: 0.1,
          },
        ],
      };
      const myChart = new Chart(ctx, {
        type: "line",
        data: data,
        options: {
          scales: {
            y: {
              beginAtZero: true,
            },
          },
        },
      });
      myChart;
    },
  },
});
</script>
