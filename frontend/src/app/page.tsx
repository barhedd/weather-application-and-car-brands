import WeatherSearch from "@/features/weather/components/WeatherSearch"

export default function Home() {
  return (
    <main className="flex flex-col items-center justify-center min-h-screen bg-gray-300 p-6">

      <h1 className="text-3xl font-bold mb-6">
        Weather App
      </h1>

      <WeatherSearch />

    </main>
  )
}