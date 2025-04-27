import { Component } from '@angular/core';
import { WeatherData } from './weather-data';
import { WeatherSearchComponent } from './weather-search/weather-search.component';
import { WeatherResultComponent } from './weather-result/weather-result.component';
import { HeaderComponent } from './header/header.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    WeatherSearchComponent,
    WeatherResultComponent,
    HeaderComponent
  ],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  weatherList: WeatherData[] = [];

  updateWeatherList(data: WeatherData[]) {
    this.weatherList = data;
  }
}
