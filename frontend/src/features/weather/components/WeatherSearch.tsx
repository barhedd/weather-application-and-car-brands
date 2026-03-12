"use client"

import { useState } from "react"
import WeatherCard from "./WeatherCard"
import { fetchWeather } from "../services/fetchWeather"
import { WeatherData } from "../types/weather.types"

export default function WeatherSearch() {
  const [city, setCity] = useState("")
  const [weather, setWeather] = useState<WeatherData | null>(null)
  const [error, setError] = useState("")

  const handleSearch = async () => {
    try {
      setError("")
      const data = await fetchWeather(city)
      setWeather(data)
    } catch {
      setWeather(null)
      setError("City not found")
    }
  }

  return (
    <div className="flex flex-col items-center">

      <div className="flex gap-2">
        <input
          className="border rounded-lg p-2"
          placeholder="Enter city"
          value={city}
          onChange={(e) => setCity(e.target.value)}
        />

        <button
          onClick={handleSearch}
          className="bg-blue-500 text-white px-4 py-2 rounded-lg"
        >
          Search
        </button>
      </div>

      {error && <p className="text-red-500 mt-4">{error}</p>}

      {weather && <WeatherCard {...weather} />}

    </div>
  )
}