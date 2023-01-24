<template>
  <div>
    <canvas id="myChart"></canvas>
    {{ errors }}
  </div>
</template>

<script>
import Chart from "chart.js/auto";

export default {
  name: "Chart",
  props: {
    errorPosition: {},
    errors: Number,
  },
  mounted() {
    const ctx = document.getElementById("myChart");
    const labels = [
      "0",
      "50",
      "100",
      "150",
      "200",
      "250",
      "300",
      "350",
      "400",
      "450",
      "500",
    ];
    const data = {
      labels: labels,
      datasets: [
        {
          label: "My First Dataset",
          // data: [65, 59, 80, 81, 56, 55, 40],
          data: this.errorPosition,
          fill: false,
          borderColor: "rgb(75, 192, 192)",
          tension: 1,
        },
      ],
    };
    const myChart = new Chart(ctx, {
      type: "line",
      data: data,
    });
    myChart;
    // const labels = [
    //   "0",
    //   "50",
    //   "100",
    //   "150",
    //   "200",
    //   "250",
    //   "300",
    //   "350",
    //   "400",
    //   "450",
    //   "500",
    // ];
    // const data = {
    //   labels: labels,
    //   datasets: [
    //     {
    //       label: "Popełnione błędy",
    //       data: [
    //         { x: 10, y: 2 },
    //         { x: 50, y: 0 },
    //         { x: 100, y: 10 },
    //         { x: 240, y: 10 },
    //       ],
    //       fill: true,
    //       borderColor: "rgb(192,75,75)",
    //       tension: 1,
    //     },
    //   ],
    // };
  },
};
</script>
