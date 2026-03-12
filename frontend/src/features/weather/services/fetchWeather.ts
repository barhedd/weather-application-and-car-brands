import { WeatherData } from "../types/weather.types"

export async function fetchWeather(city: string): Promise<WeatherData> {
  try {
    const res = await fetch(`/api/weather?city=${encodeURIComponent(city)}`)

    if (!res.ok) {
      if (res.status === 404) {
        throw new Error("Ciudad no encontrada")
      } else {
        throw new Error("Error al obtener los datos del clima")
      }
    }

    const data: WeatherData = await res.json()
    return data
  } catch (err: any) {
    if (err instanceof TypeError) {
      // Error de red
      throw new Error("Error de red. Verifica tu conexión")
    }
    throw err
  }
}