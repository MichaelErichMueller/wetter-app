import { Component, Output, EventEmitter } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { WeatherService } from '../weather.service';
import { WeatherData } from '../weather-data';
import { catchError } from 'rxjs/operators';
import { of } from 'rxjs';

@Component({
  selector: 'app-weather-search',
  templateUrl: './weather-search.component.html',
  styleUrls: ['./weather-search.component.css'],
  standalone: true,
  imports: [FormsModule, CommonModule]
})
export class WeatherSearchComponent {
  city: string = '';
  weatherList: WeatherData[] = [];
  errorMessage: string = '';

  @Output() weatherListUpdated = new EventEmitter<WeatherData[]>();

  constructor(private weatherService: WeatherService) {}

  searchWeather() {
    this.errorMessage = '';
    if (!this.city.trim()) {
      this.errorMessage = 'Bitte gib eine Stadt ein.';
      this.weatherList = [];
      return;
    }

    this.weatherService.getWeather(this.city).pipe(
      catchError((error) => {
        console.error('Fehler beim Abrufen der Wetterdaten:', error);
        this.weatherList = [];
        this.errorMessage = 'Rechtschreibfehler! Bitte überprüfen sie ihre Eingabe.';
        this.weatherListUpdated.emit([]);
        return of(null);
      })
    ).subscribe((data) => {
      if (data) {
        const limitedData = data.slice(0, 4);
        this.weatherList = limitedData;
        this.weatherListUpdated.emit(this.weatherList);
      }
    });
  }
}
