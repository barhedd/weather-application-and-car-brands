import { WeatherData } from "../types/weather.types"

export default function WeatherCard({
  city,
  temperature,
  humidity,
  description,
}: WeatherData) {
  return (
    <div className="bg-white shadow-md rounded-xl p-6 mt-6 w-full max-w-md">
      <h2 className="text-xl font-semibold mb-3">Clima en la ciudad de {city}</h2>

      <p>Temperatura: {temperature} °C</p>
      <p>Humedad: {humidity}%</p>
      <p>Descripción: {description}</p>
    </div>
  )
}