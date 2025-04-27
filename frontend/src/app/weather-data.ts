export interface WeatherData {
  date: Date;
  periods: {
    period: string;
    time: string;
    temperature: number;
    description: string;
    precipitationProbability: number;
  }[];
}
