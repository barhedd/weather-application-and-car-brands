import { render, screen } from "@testing-library/react";
import WeatherCard from "@/features/weather/components/WeatherCard";

describe("WeatherCard Component", () => {
  const mockData = {
    city: "Madrid",
    temperature: 25,
    humidity: 50,
    description: "Soleado",
  };

  it("debería renderizar correctamente los datos del clima", () => {
    render(<WeatherCard {...mockData} />);

    expect(screen.getByText(`Clima en la ciudad de ${mockData.city}`)).toBeInTheDocument();
    expect(screen.getByText(`Temperatura: ${mockData.temperature} °C`)).toBeInTheDocument();
    expect(screen.getByText(`Humedad: ${mockData.humidity}%`)).toBeInTheDocument();
    expect(screen.getByText(`Descripción: ${mockData.description}`)).toBeInTheDocument();
  });
});