import { WeatherData } from "../types/weather.types"

export default function WeatherCard({
  temperature,
  humidity,
  description,
}: WeatherData) {
  return (
    <div className="bg-white shadow-md rounded-xl p-6 mt-6 w-full max-w-md">
      <h2 className="text-xl font-semibold mb-3">Weather</h2>

      <p>Temperature: {temperature} °C</p>
      <p>Humidity: {humidity}%</p>
      <p>Description: {description}</p>
    </div>
  )
}