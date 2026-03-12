import { render, screen } from "@testing-library/react"
import WeatherCard from "@/features/weather/components/WeatherCard"

describe("WeatherCard Component", () => {
  const mockData = {
    city: "Madrid",
    temperature: 25,
    humidity: 50,
    description: "Soleado"
  }

  it("debería renderizar correctamente los datos del clima", () => {
    render(<WeatherCard {...mockData} />)

    // Verifica el título con la ciudad
    expect(screen.getByText(`Clima en la ciudad de ${mockData.city}`)).toBeInTheDocument()

    // Verifica la temperatura
    expect(screen.getByText(`Temperatura: ${mockData.temperature} °C`)).toBeInTheDocument()

    // Verifica la humedad
    expect(screen.getByText(`Humedad: ${mockData.humidity}%`)).toBeInTheDocument()

    // Verifica la descripción
    expect(screen.getByText(`Descripción: ${mockData.description}`)).toBeInTheDocument()
  })
})