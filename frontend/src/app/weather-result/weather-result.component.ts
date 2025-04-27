import { Component, Input } from '@angular/core';
import { WeatherData } from '../weather-data';
import { CommonModule } from '@angular/common'; 

@Component({
  selector: 'app-weather-result',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './weather-result.component.html',
  styleUrls: ['./weather-result.component.css']
})
export class WeatherResultComponent {
  @Input() weatherList: WeatherData[] = [];
}