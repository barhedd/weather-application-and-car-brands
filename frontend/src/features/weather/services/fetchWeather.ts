import { WeatherData } from "../types/weather.types";

export async function fetchWeather(city: string): Promise<WeatherData> {
  const res = await fetch(`/api/weather?city=${city}`)

  if (!res.ok) {
    throw new Error("City not found")
  }

  return res.json()
}