import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import { WeatherData } from './weather-data';

@Injectable({
  providedIn: 'root'
})
export class WeatherService {
  constructor(private http: HttpClient) {}

  getWeather(city: string): Observable<WeatherData[]> {
    const query = `
      query GetWeather($city: String!) {
        weather(city: $city) {
          date
          periods {
            period
            time
            temperature
            description
            precipitationProbability
          }
        }
      }
    `;

    const body = {
      query,
      variables: { city }
    };

    return this.http
      .post<{ data: { weather: WeatherData[] } }>('http://localhost:5000/graphql', body)
      .pipe(
        map(response =>
          response.data.weather.map(entry => ({
            ...entry,
            date: new Date(entry.date)
          }))
        ),
      );
  }
}
